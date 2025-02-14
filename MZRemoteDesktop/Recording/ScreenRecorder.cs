using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Streams;
using Xabe.FFmpeg.Streams.SubtitleStream;
using Newtonsoft.Json;
using Xabe.FFmpeg.Downloader;

public class ScreenRecorder : IDisposable
{
    private readonly Settings settings;
    private readonly string currentVideoPath;
    private readonly string currentMetadataPath;
    private RecordingMetadata metadata;
    private CancellationTokenSource cancellationSource;
    private bool isPaused;
    private Task recordingTask;
    private readonly object lockObject = new object();
    private Bitmap previousFrame;
    private IConversion currentConversion;
    private bool isConverting;

    public ScreenRecorder(Settings settings)
    {
        this.settings = settings;
        string dateFolder = DateTime.Now.ToString("yyyy-MM-dd");
        string baseDir = Path.Combine(settings.RecordingDirectory, dateFolder);
        Directory.CreateDirectory(baseDir);

        string timestamp = DateTime.Now.ToString("HH-mm-ss");
        currentVideoPath = Path.Combine(baseDir, $"recording_{timestamp}.mp4");
        currentMetadataPath = Path.Combine(baseDir, $"recording_{timestamp}.json");

        metadata = new RecordingMetadata
        {
            VideoFilePath = currentVideoPath,
            StartTime = DateTime.Now
        };

        // Initialize FFmpeg
        string ffmpegPath = settings.FFmpegPath;
        if (!Directory.Exists(ffmpegPath))
        {
            ffmpegPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg");
            Directory.CreateDirectory(ffmpegPath);
        }
        FFmpeg.SetExecutablesPath(ffmpegPath);
    }

    public void Start()
    {
        lock (lockObject)
        {
            if (recordingTask != null) return;

            cancellationSource = new CancellationTokenSource();
            isPaused = false;
            recordingTask = Task.Run(RecordingLoopAsync);
        }
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Resume()
    {
        isPaused = false;
    }

    public void Stop()
    {
        lock (lockObject)
        {
            if (recordingTask == null) return;

            cancellationSource.Cancel();
            recordingTask.Wait();
            recordingTask = null;

            metadata.EndTime = DateTime.Now;
            string json = JsonConvert.SerializeObject(metadata, Formatting.Indented);
            File.WriteAllText(currentMetadataPath, json);

            // Wait for any ongoing conversion to complete
            while (isConverting)
            {
                Thread.Sleep(100);
            }
            previousFrame?.Dispose();
        }
    }

    private async Task RecordingLoopAsync()
    {
        try
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "ScreenRecorder");
            Directory.CreateDirectory(tempDir);

            int frameCount = 0;
            DateTime startTime = DateTime.Now;

            while (!cancellationSource.Token.IsCancellationRequested)
            {
                if (!isPaused)
                {
                    using (var bitmap = CaptureScreen())
                    {
                        if (ShouldRecordFrame(bitmap))
                        {
                            string framePath = Path.Combine(tempDir, $"frame_{frameCount:D6}.png");
                            bitmap.Save(framePath, ImageFormat.Png);
                            frameCount++;
                        }
                    }

                    // Check if we need to split the video
                    if ((DateTime.Now - startTime).TotalMinutes >= settings.VideoSplitDurationMinutes)
                    {
                        await CreateVideoFromFrames(tempDir, currentVideoPath);
                        Directory.Delete(tempDir, true);
                        Directory.CreateDirectory(tempDir);
                        frameCount = 0;
                        startTime = DateTime.Now;
                    }

                    Thread.Sleep(settings.FrameRateInSeconds * 1000);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }

            // Create final video if there are any frames
            if (frameCount > 0)
            {
                await CreateVideoFromFrames(tempDir, currentVideoPath);
                Directory.Delete(tempDir, true);
            }
        }
        catch (Exception ex)
        {
            // Log error
            Console.WriteLine($"Recording error: {ex.Message}");
        }
    }

    private Bitmap CaptureScreen()
    {
        var bounds = Screen.PrimaryScreen.Bounds;
        var bitmap = new Bitmap(bounds.Width, bounds.Height);
        using (var graphics = Graphics.FromImage(bitmap))
        {
            graphics.CopyFromScreen(0, 0, 0, 0, bounds.Size);
        }
        return bitmap;
    }

    private bool ShouldRecordFrame(Bitmap currentFrame)
    {
        if (!settings.DetectFrameChanges || previousFrame == null)
        {
            if (previousFrame != null)
                previousFrame.Dispose();
            previousFrame = new Bitmap(currentFrame);
            return true;
        }

        bool shouldRecord = false;
        int changedPixels = 0;
        int totalPixels = currentFrame.Width * currentFrame.Height;

        BitmapData currentData = null;
        BitmapData previousData = null;

        try
        {
            currentData = currentFrame.LockBits(
                new Rectangle(0, 0, currentFrame.Width, currentFrame.Height),
                ImageLockMode.ReadOnly, 
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            previousData = previousFrame.LockBits(
                new Rectangle(0, 0, previousFrame.Width, previousFrame.Height),
                ImageLockMode.ReadOnly, 
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* currentScan = (byte*)currentData.Scan0;
                byte* previousScan = (byte*)previousData.Scan0;

                for (int i = 0; i < currentData.Stride * currentData.Height; i += 4)
                {
                    if (currentScan[i] != previousScan[i] ||
                        currentScan[i + 1] != previousScan[i + 1] ||
                        currentScan[i + 2] != previousScan[i + 2])
                    {
                        changedPixels++;
                    }
                }
            }
        }
        finally
        {
            if (currentData != null)
                currentFrame.UnlockBits(currentData);
            if (previousData != null)
                previousFrame.UnlockBits(previousData);
        }

        double changePercentage = (double)changedPixels / totalPixels;
        shouldRecord = changePercentage >= settings.ChangeThreshold;

        if (shouldRecord)
        {
            previousFrame.Dispose();
            previousFrame = new Bitmap(currentFrame);
        }

        return shouldRecord;
    }

    private async Task CreateVideoFromFrames(string framesDirectory, string outputPath)
    {
        isConverting = true;
        try
        {
            var builder = new ConversionBuilder()
                .SetInput(Path.Combine(framesDirectory, "frame_%06d.png"))
                .SetFrameRate(settings.FrameRateInSeconds)
                .SetOutput(outputPath)
                .SetPixelFormat(Xabe.FFmpeg.PixelFormat.yuv420p);

            currentConversion = builder.Build();

            currentConversion.SetPreset(GetPreset(settings.CompressionPreset));
            if (settings.EnableHardwareEncoding)
            {
                try
                {
                    currentConversion.UseHardwareAcceleration(
                        settings.PreferredAccelerator,
                        settings.PreferredCodec,
                        GetHardwareCodec(settings.PreferredAccelerator));
                }
                catch (Exception ex)
                {
                    // If hardware acceleration fails, continue without it
                    Console.WriteLine($"Hardware acceleration not available, using software encoding: {ex.Message}");
                }
            }

            await currentConversion.Start();
        }
        finally
        {
            isConverting = false;
            currentConversion = null;
        }
    }

    private ConversionPreset GetPreset(int preset)
    {
        switch (preset)
        {
            case 0: return ConversionPreset.VerySlow;
            case 1: return ConversionPreset.Slower;
            case 2: return ConversionPreset.Slow;
            case 3: return ConversionPreset.Medium;
            case 4: return ConversionPreset.Fast;
            case 5: return ConversionPreset.VeryFast;
            default: return ConversionPreset.Medium;
        }
    }

    private VideoCodec GetHardwareCodec(HardwareAccelerator accelerator)
    {
        switch (accelerator)
        {
            case HardwareAccelerator.Cuda:
                return VideoCodec.H264_nvenc;
            case HardwareAccelerator.QuickSync:
                return VideoCodec.H264_qsv;
            case HardwareAccelerator.Amd:
                return VideoCodec.H264_amf;
            default:
                return VideoCodec.H264;
        }
    }

    public void AddBookmark(string text)
    {
        metadata.Bookmarks.Add(new Bookmark
        {
            Timestamp = DateTime.Now,
            Text = text,
            VideoPosition = DateTime.Now - metadata.StartTime
        });
    }

    public void UpdateWindowTitle(string title)
    {
        metadata.WindowEvents.Add(new WindowEvent
        {
            Timestamp = DateTime.Now,
            WindowTitle = title
        });
    }

    public void UpdateCursorPosition(Point position)
    {
        metadata.CursorEvents.Add(new CursorEvent
        {
            Timestamp = DateTime.Now,
            X = position.X,
            Y = position.Y
        });
    }

    public void UpdateClipboardContent(string content)
    {
        metadata.ClipboardEvents.Add(new ClipboardEvent
        {
            Timestamp = DateTime.Now,
            Content = content
        });
    }

    public void Dispose()
    {
        Stop();
        previousFrame?.Dispose();
    }
} 
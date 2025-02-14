using System;
using Newtonsoft.Json;
using System.IO;
using Xabe.FFmpeg;

public class Settings
{
    // Recording Settings
    public int FrameRateInSeconds { get; set; } = 1;
    public int VideoSplitDurationMinutes { get; set; } = 30;
    public string RecordingDirectory { get; set; }
    public bool StartWithWindows { get; set; }
    public int VideoQuality { get; set; } = 23; // Lower is better quality (18-28 is good range)
    public bool DetectFrameChanges { get; set; } = true;
    public double ChangeThreshold { get; set; } = 0.02; // 2% change threshold
    public string FFmpegPath { get; set; }

    // Hotkey Settings
    public bool EnableBookmarkHotkey { get; set; } = true;
    public Keys BookmarkHotkey { get; set; } = Keys.B;
    public ModifierKeys BookmarkModifier { get; set; } = ModifierKeys.Control;

    // Compression Settings
    public int CompressionPreset { get; set; } = 3; // 0-5 (0=slowest/best, 5=fastest/worst)
    public bool EnableHardwareEncoding { get; set; } = true;
    public HardwareAccelerator PreferredAccelerator { get; set; } = HardwareAccelerator.Auto;
    public VideoCodec PreferredCodec { get; set; } = VideoCodec.H264;

    private static readonly string SettingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "ScreenRecorder",
        "settings.json"
    );

    public static Settings Load()
    {
        try
        {
            if (File.Exists(SettingsPath))
            {
                string json = File.ReadAllText(SettingsPath);
                var settings = JsonConvert.DeserializeObject<Settings>(json);
                if (settings != null)
                {
                    // Set FFmpeg path if not already set
                    if (string.IsNullOrEmpty(settings.FFmpegPath))
                    {
                        settings.FFmpegPath = Path.Combine(
                            AppDomain.CurrentDomain.BaseDirectory,
                            "ffmpeg"
                        );
                    }
                    return settings;
                }
            }
        }
        catch (Exception)
        {
            // Log error if needed
        }
        return CreateDefault();
    }

    public void Save()
    {
        try
        {
            string directoryPath = Path.GetDirectoryName(SettingsPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(SettingsPath, json);
        }
        catch (Exception)
        {
            // Log error if needed
        }
    }

    private static Settings CreateDefault()
    {
        return new Settings
        {
            RecordingDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
                "ScreenRecorder"
            ),
            FFmpegPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "ffmpeg"
            )
        };
    }
}

[Flags]
public enum ModifierKeys
{
    None = 0,
    Alt = 1,
    Control = 2,
    Shift = 4,
    Windows = 8
} 
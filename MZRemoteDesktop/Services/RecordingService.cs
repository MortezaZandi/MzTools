using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

public class RecordingService : IDisposable
{
    private readonly Settings settings;
    private ScreenRecorder currentRecorder;
    private Timer splitTimer;
    private Timer titleCheckTimer;
    private string lastWindowTitle;

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder text, int count);

    public RecordingService(Settings settings)
    {
        this.settings = settings;
        InitializeTimers();
    }

    private void InitializeTimers()
    {
        splitTimer = new Timer
        {
            Interval = settings.VideoSplitDurationMinutes * 60 * 1000
        };
        splitTimer.Tick += SplitRecording;

        titleCheckTimer = new Timer
        {
            Interval = 1000 // Check window title every second
        };
        titleCheckTimer.Tick += CheckActiveWindow;
    }

    public void StartRecording()
    {
        StopRecording();
        currentRecorder = new ScreenRecorder(settings);
        currentRecorder.Start();
        splitTimer.Start();
        titleCheckTimer.Start();
    }

    public void PauseRecording()
    {
        currentRecorder?.Pause();
        splitTimer.Stop();
        titleCheckTimer.Stop();
    }

    public void ResumeRecording()
    {
        currentRecorder?.Resume();
        splitTimer.Start();
        titleCheckTimer.Start();
    }

    public void StopRecording()
    {
        if (currentRecorder != null)
        {
            currentRecorder.Stop();
            currentRecorder.Dispose();
            currentRecorder = null;
        }
        splitTimer.Stop();
        titleCheckTimer.Stop();
    }

    private void SplitRecording(object sender, EventArgs e)
    {
        if (currentRecorder != null)
        {
            var oldRecorder = currentRecorder;
            StartRecording(); // This creates a new recorder
            oldRecorder.Stop();
            oldRecorder.Dispose();
        }
    }

    private void CheckActiveWindow(object sender, EventArgs e)
    {
        string currentTitle = GetActiveWindowTitle();
        if (currentTitle != lastWindowTitle)
        {
            lastWindowTitle = currentTitle;
            currentRecorder?.UpdateWindowTitle(currentTitle);
        }
    }

    private string GetActiveWindowTitle()
    {
        IntPtr handle = GetForegroundWindow();
        var buff = new System.Text.StringBuilder(500);
        GetWindowText(handle, buff, buff.Capacity);
        return buff.ToString();
    }

    public void AddBookmark(string text)
    {
        currentRecorder?.AddBookmark(text);
    }

    public void Dispose()
    {
        StopRecording();
        splitTimer?.Dispose();
        titleCheckTimer?.Dispose();
    }
} 
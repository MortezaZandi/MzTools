using System;
using System.Windows.Forms;
using System.Drawing;

public class MainForm : Form
{
    private NotifyIcon trayIcon;
    private ContextMenuStrip trayMenu;
    private Settings settings;
    private bool isRecording;

    public MainForm()
    {
        settings = Settings.Load();
        InitializeComponents();
        InitializeTrayIcon();
    }

    private void InitializeComponents()
    {
        this.ShowInTaskbar = false;
        this.WindowState = FormWindowState.Minimized;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Text = "Screen Recorder";
    }

    private void InitializeTrayIcon()
    {
        trayMenu = new ContextMenuStrip();
        trayMenu.Items.Add("Start Recording", null, OnStartRecording);
        trayMenu.Items.Add("Pause Recording", null, OnPauseRecording);
        trayMenu.Items.Add("Stop Recording", null, OnStopRecording);
        trayMenu.Items.Add("-"); // Separator
        trayMenu.Items.Add("Open", null, OnOpenMainWindow);
        trayMenu.Items.Add("Settings", null, OnOpenSettings);
        trayMenu.Items.Add("-");
        trayMenu.Items.Add("Exit", null, OnExit);

        trayIcon = new NotifyIcon
        {
            Text = "Screen Recorder",
            Icon = SystemIcons.Application, // Replace with your custom icon
            ContextMenuStrip = trayMenu,
            Visible = true
        };

        trayIcon.DoubleClick += OnOpenMainWindow;
        UpdateMenuState(false);
    }

    private void UpdateMenuState(bool recording)
    {
        isRecording = recording;
        trayMenu.Items[0].Enabled = !recording; // Start
        trayMenu.Items[1].Enabled = recording;  // Pause
        trayMenu.Items[2].Enabled = recording;  // Stop
    }

    private void OnStartRecording(object sender, EventArgs e)
    {
        // TODO: Implement recording start
        UpdateMenuState(true);
    }

    private void OnPauseRecording(object sender, EventArgs e)
    {
        // TODO: Implement recording pause
    }

    private void OnStopRecording(object sender, EventArgs e)
    {
        // TODO: Implement recording stop
        UpdateMenuState(false);
    }

    private void OnOpenMainWindow(object sender, EventArgs e)
    {
        this.WindowState = FormWindowState.Normal;
        this.Show();
        this.BringToFront();
    }

    private void OnOpenSettings(object sender, EventArgs e)
    {
        using (var settingsForm = new SettingsForm(settings))
        {
            settingsForm.ShowDialog(this);
        }
    }

    private void OnExit(object sender, EventArgs e)
    {
        if (isRecording)
        {
            DialogResult result = MessageBox.Show(
                "Recording is in progress. Are you sure you want to exit?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;
        }

        trayIcon.Visible = false;
        Application.Exit();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }
        else
        {
            base.OnFormClosing(e);
        }
    }
} 
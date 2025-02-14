using System;
using System.Windows.Forms;

public class SettingsForm : Form
{
    private Settings settings;
    private NumericUpDown frameRateUpDown;
    private NumericUpDown splitDurationUpDown;
    private TextBox recordingPathTextBox;
    private CheckBox startWithWindowsCheckBox;
    private TrackBar qualityTrackBar;

    public SettingsForm(Settings settings)
    {
        this.settings = settings;
        InitializeComponents();
        LoadSettings();
    }

    private void InitializeComponents()
    {
        this.Text = "Screen Recorder Settings";
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.StartPosition = FormStartPosition.CenterParent;
        this.Width = 400;
        this.Height = 300;

        var y = 20;
        const int padding = 10;
        const int labelWidth = 150;
        const int controlWidth = 200;
        const int controlHeight = 23;

        // Frame Rate
        var frameRateLabel = new Label
        {
            Text = "Frame Rate (seconds):",
            Left = padding,
            Top = y,
            Width = labelWidth
        };
        this.Controls.Add(frameRateLabel);

        frameRateUpDown = new NumericUpDown
        {
            Left = labelWidth + padding * 2,
            Top = y,
            Width = controlWidth,
            Minimum = 1,
            Maximum = 60
        };
        this.Controls.Add(frameRateUpDown);

        y += controlHeight + padding;

        // Split Duration
        var splitDurationLabel = new Label
        {
            Text = "Split Duration (minutes):",
            Left = padding,
            Top = y,
            Width = labelWidth
        };
        this.Controls.Add(splitDurationLabel);

        splitDurationUpDown = new NumericUpDown
        {
            Left = labelWidth + padding * 2,
            Top = y,
            Width = controlWidth,
            Minimum = 1,
            Maximum = 120
        };
        this.Controls.Add(splitDurationUpDown);

        y += controlHeight + padding;

        // Recording Path
        var recordingPathLabel = new Label
        {
            Text = "Recording Path:",
            Left = padding,
            Top = y,
            Width = labelWidth
        };
        this.Controls.Add(recordingPathLabel);

        recordingPathTextBox = new TextBox
        {
            Left = labelWidth + padding * 2,
            Top = y,
            Width = controlWidth - 30
        };
        this.Controls.Add(recordingPathTextBox);

        var browseButton = new Button
        {
            Text = "...",
            Left = labelWidth + padding * 2 + controlWidth - 25,
            Top = y,
            Width = 25,
            Height = controlHeight
        };
        browseButton.Click += OnBrowseClick;
        this.Controls.Add(browseButton);

        y += controlHeight + padding;

        // Start with Windows
        startWithWindowsCheckBox = new CheckBox
        {
            Text = "Start with Windows",
            Left = padding,
            Top = y,
            Width = labelWidth + controlWidth
        };
        this.Controls.Add(startWithWindowsCheckBox);

        y += controlHeight + padding;

        // Video Quality
        var qualityLabel = new Label
        {
            Text = "Video Quality:",
            Left = padding,
            Top = y,
            Width = labelWidth
        };
        this.Controls.Add(qualityLabel);

        qualityTrackBar = new TrackBar
        {
            Left = labelWidth + padding * 2,
            Top = y,
            Width = controlWidth,
            Minimum = 0,
            Maximum = 51,
            TickFrequency = 5
        };
        this.Controls.Add(qualityTrackBar);

        y += 50;

        // Buttons
        var saveButton = new Button
        {
            Text = "Save",
            DialogResult = DialogResult.OK,
            Left = this.Width - 170,
            Top = y,
            Width = 75
        };
        this.Controls.Add(saveButton);

        var cancelButton = new Button
        {
            Text = "Cancel",
            DialogResult = DialogResult.Cancel,
            Left = this.Width - 90,
            Top = y,
            Width = 75
        };
        this.Controls.Add(cancelButton);
    }

    private void LoadSettings()
    {
        frameRateUpDown.Value = settings.FrameRateInSeconds;
        splitDurationUpDown.Value = settings.VideoSplitDurationMinutes;
        recordingPathTextBox.Text = settings.RecordingDirectory;
        startWithWindowsCheckBox.Checked = settings.StartWithWindows;
        qualityTrackBar.Value = settings.VideoQuality;
    }

    private void OnBrowseClick(object sender, EventArgs e)
    {
        using (var dialog = new FolderBrowserDialog())
        {
            dialog.SelectedPath = recordingPathTextBox.Text;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                recordingPathTextBox.Text = dialog.SelectedPath;
            }
        }
    }

    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
        if (this.DialogResult == DialogResult.OK)
        {
            settings.FrameRateInSeconds = (int)frameRateUpDown.Value;
            settings.VideoSplitDurationMinutes = (int)splitDurationUpDown.Value;
            settings.RecordingDirectory = recordingPathTextBox.Text;
            settings.StartWithWindows = startWithWindowsCheckBox.Checked;
            settings.VideoQuality = qualityTrackBar.Value;
            settings.Save();
        }
        base.OnClosing(e);
    }
} 
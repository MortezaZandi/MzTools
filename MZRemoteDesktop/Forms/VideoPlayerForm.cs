using System;
using System.Windows.Forms;
using AxWMPLib;
using System.IO;
using Newtonsoft.Json;

public class VideoPlayerForm : Form
{
    private AxWindowsMediaPlayer mediaPlayer;
    private ListBox videoListBox;
    private Button playButton;
    private Button pauseButton;
    private Button stopButton;
    private Button loadVideosButton;
    private Label bookmarksLabel;
    private ListBox bookmarksListBox;

    public VideoPlayerForm()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        this.Text = "Video Player";
        this.Width = 800;
        this.Height = 600;

        mediaPlayer = new AxWindowsMediaPlayer();
        mediaPlayer.Dock = DockStyle.Top;
        mediaPlayer.Height = 300;
        this.Controls.Add(mediaPlayer);

        videoListBox = new ListBox
        {
            Left = 10,
            Top = 310,
            Width = 200,
            Height = 200
        };
        this.Controls.Add(videoListBox);

        playButton = new Button
        {
            Text = "Play",
            Left = 220,
            Top = 310,
            Width = 75
        };
        playButton.Click += OnPlayButtonClick;
        this.Controls.Add(playButton);

        pauseButton = new Button
        {
            Text = "Pause",
            Left = 300,
            Top = 310,
            Width = 75
        };
        pauseButton.Click += OnPauseButtonClick;
        this.Controls.Add(pauseButton);

        stopButton = new Button
        {
            Text = "Stop",
            Left = 380,
            Top = 310,
            Width = 75
        };
        stopButton.Click += OnStopButtonClick;
        this.Controls.Add(stopButton);

        loadVideosButton = new Button
        {
            Text = "Load Videos",
            Left = 460,
            Top = 310,
            Width = 100
        };
        loadVideosButton.Click += OnLoadVideosButtonClick;
        this.Controls.Add(loadVideosButton);

        bookmarksLabel = new Label
        {
            Text = "Bookmarks:",
            Left = 10,
            Top = 520,
            Width = 100
        };
        this.Controls.Add(bookmarksLabel);

        bookmarksListBox = new ListBox
        {
            Left = 10,
            Top = 540,
            Width = 200,
            Height = 100
        };
        this.Controls.Add(bookmarksListBox);
    }

    private void OnPlayButtonClick(object sender, EventArgs e)
    {
        if (videoListBox.SelectedItem != null)
        {
            mediaPlayer.URL = videoListBox.SelectedItem.ToString();
            mediaPlayer.Ctlcontrols.play();
            LoadBookmarks(videoListBox.SelectedItem.ToString());
        }
    }

    private void OnPauseButtonClick(object sender, EventArgs e)
    {
        mediaPlayer.Ctlcontrols.pause();
    }

    private void OnStopButtonClick(object sender, EventArgs e)
    {
        mediaPlayer.Ctlcontrols.stop();
    }

    private void OnLoadVideosButtonClick(object sender, EventArgs e)
    {
        videoListBox.Items.Clear();
        string[] videoFiles = Directory.GetFiles(Settings.Load().RecordingDirectory, "*.mp4", SearchOption.AllDirectories);
        videoListBox.Items.AddRange(videoFiles);
    }

    private void LoadBookmarks(string videoFilePath)
    {
        bookmarksListBox.Items.Clear();
        string metadataFilePath = Path.ChangeExtension(videoFilePath, ".json");
        if (File.Exists(metadataFilePath))
        {
            string json = File.ReadAllText(metadataFilePath);
            var metadata = JsonConvert.DeserializeObject<RecordingMetadata>(json);
            foreach (var bookmark in metadata.Bookmarks)
            {
                bookmarksListBox.Items.Add($"{bookmark.Timestamp}: {bookmark.Text}");
            }
        }
    }
} 
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class RecordingMetadata
{
    public string VideoFilePath { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<WindowEvent> WindowEvents { get; set; } = new List<WindowEvent>();
    public List<CursorEvent> CursorEvents { get; set; } = new List<CursorEvent>();
    public List<ClipboardEvent> ClipboardEvents { get; set; } = new List<ClipboardEvent>();
    public List<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
}

public class WindowEvent
{
    public DateTime Timestamp { get; set; }
    public string WindowTitle { get; set; }
}

public class CursorEvent
{
    public DateTime Timestamp { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}

public class ClipboardEvent
{
    public DateTime Timestamp { get; set; }
    public string Content { get; set; }
}

public class Bookmark
{
    public DateTime Timestamp { get; set; }
    public string Text { get; set; }
    public TimeSpan VideoPosition { get; set; }
} 
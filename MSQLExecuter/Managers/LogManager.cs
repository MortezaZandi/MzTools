using System;
using System.IO;

public class LogManager
{
    private static readonly string LogPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "SQLExecutor", "logs");
    
    private static readonly object _lock = new object();
    
    static LogManager()
    {
        Directory.CreateDirectory(LogPath);
    }
    
    public static void Log(string message, LogLevel level = LogLevel.Info)
    {
        var logFile = Path.Combine(LogPath, $"{DateTime.Now:yyyy-MM-dd}.log");
        var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
        
        lock (_lock)
        {
            File.AppendAllText(logFile, logMessage + Environment.NewLine);
        }
    }
    
    public static void LogException(Exception ex, string context = null)
    {
        var message = $"{context ?? "Error"}: {ex.Message}\n{ex.StackTrace}";
        Log(message, LogLevel.Error);
    }
}

public enum LogLevel
{
    Debug,
    Info,
    Warning,
    Error
} 
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;
using System.Linq;
using System.Windows.Forms;

public class SettingsManager
{
    private const string SettingsFileName = "settings.json";
    private static string SettingsPath => 
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "SQLExecutor", SettingsFileName);
    
    public class AppSettings
    {
        public List<ConnectionInfo> SavedConnections { get; set; } = new List<ConnectionInfo>();
        public string LastUsedConnection { get; set; }
        public Dictionary<string, string> RecentFiles { get; set; } = new Dictionary<string, string>();
        public Font EditorFont { get; set; }
        public bool ShowLineNumbers { get; set; } = true;
        public bool AutoSaveEnabled { get; set; } = true;
        public int AutoSaveIntervalMinutes { get; set; } = 5;
    }
    
    private static AppSettings _current;
    
    public static AppSettings Current
    {
        get
        {
            if (_current == null)
            {
                Load();
            }
            return _current;
        }
    }
    
    public static void Load()
    {
        try
        {
            if (File.Exists(SettingsPath))
            {
                var json = File.ReadAllText(SettingsPath);
                _current = JsonConvert.DeserializeObject<AppSettings>(json);
            }
            else
            {
                _current = new AppSettings();
            }
        }
        catch (Exception)
        {
            _current = new AppSettings();
        }
    }
    
    public static void Save()
    {
        try
        {
            var directory = Path.GetDirectoryName(SettingsPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            var json = JsonConvert.SerializeObject(_current, Formatting.Indented);
            File.WriteAllText(SettingsPath, json);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving settings: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    public static void AddRecentFile(string filePath)
    {
        const int maxRecentFiles = 10;
        
        var fileName = Path.GetFileName(filePath);
        Current.RecentFiles[fileName] = filePath;
        
        if (Current.RecentFiles.Count > maxRecentFiles)
        {
            Current.RecentFiles = Current.RecentFiles
                .Skip(Current.RecentFiles.Count - maxRecentFiles)
                .ToDictionary(x => x.Key, x => x.Value);
        }
        
        Save();
    }
} 
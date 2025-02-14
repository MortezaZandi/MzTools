using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace MSQLExecuter.Managers
{
    public class ConnectionManager
    {
        private const string ConfigFileName = "connections.json";
        private static readonly string ConfigPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MSQLExecuter",
            ConfigFileName);

        private List<ConnectionInfo> _connections = new List<ConnectionInfo>();
        public event EventHandler ConnectionsChanged;

        public ConnectionInfo CurrentConnection { get; private set; }

        public IReadOnlyList<ConnectionInfo> Connections => _connections.AsReadOnly();

        public ConnectionManager()
        {
            LoadConnections();
        }

        public void AddConnection(ConnectionInfo connection)
        {
            _connections.Add(connection);
            CurrentConnection = connection;
            SaveConnections();
            ConnectionsChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetCurrentConnection(ConnectionInfo connection)
        {
            if (_connections.Contains(connection))
            {
                CurrentConnection = connection;
                ConnectionsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void LoadConnections()
        {
            try
            {
                if (File.Exists(ConfigPath))
                {
                    var json = File.ReadAllText(ConfigPath);
                    _connections = JsonConvert.DeserializeObject<List<ConnectionInfo>>(json) ?? new List<ConnectionInfo>();
                    CurrentConnection = _connections.LastOrDefault();
                }
            }
            catch (Exception ex)
            {
                // Log error and start with empty connections
                _connections = new List<ConnectionInfo>();
            }
        }

        private void SaveConnections()
        {
            try
            {
                var directory = Path.GetDirectoryName(ConfigPath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,  // Makes the JSON file human-readable
                    NullValueHandling = NullValueHandling.Ignore  // Skips null properties
                };

                var json = JsonConvert.SerializeObject(_connections, settings);
                File.WriteAllText(ConfigPath, json);
            }
            catch (Exception ex)
            {
                // Log error
            }
        }
    }
} 
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ScriptGroup
{
    public string Name { get; set; }
    public List<Script> Scripts { get; set; } = new List<Script>();
    public bool IsExpanded { get; set; }

    public void SaveToFile(string filePath)
    {
        var data = new
        {
            Name = this.Name,
            Scripts = this.Scripts.Select(s => new { s.Name, s.Content }).ToList()
        };
        
        File.WriteAllText(filePath, JsonConvert.SerializeObject(data, Formatting.Indented));
    }

    public static ScriptGroup LoadFromFile(string filePath)
    {
        var json = File.ReadAllText(filePath);
        var data = JsonConvert.DeserializeObject<ScriptGroup>(json);
        return data;
    }
}

public class Script
{
    public string Name { get; set; }
    public string Content { get; set; }
} 
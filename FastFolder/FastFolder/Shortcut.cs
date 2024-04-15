using System;

namespace FastFolder
{
    [Serializable]
    public class Shortcut
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int SortIndex { get; set; }
        public int UsageCount { get; set; }
    }
}

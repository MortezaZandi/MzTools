using System;
using System.Collections.Generic;

namespace FastFolder
{
    [Serializable]
    public class ShortcutsContainer
    {
        public ShortcutsContainer()
        {
            this.Shortcuts = new List<Shortcut>();
        }

        public List<Shortcut> Shortcuts { get; set; }
    }
}

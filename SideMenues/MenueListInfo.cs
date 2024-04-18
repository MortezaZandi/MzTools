using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SideMenues
{
    [Serializable]
    public class MenueListInfo
    {
        public MenueListInfo()
        {
            this.MenueList = new List<MenueItemInfo>();
        }

        public List<MenueItemInfo> MenueList { get; set; }

        internal MenueListInfo CreateCopy()
        {
            var copy = new MenueListInfo();
            foreach (var item in this.MenueList)
            {
                copy.MenueList.Add(item.CreateCopy());
            }

            return copy;
        }
    }

    [Serializable]
    public class MenueItemInfo
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public string Path { get; set; }
        public int BackColor { get; set; } = Color.White.ToArgb();
        public int TextColor { get; set; } = Color.Black.ToArgb();
        public bool IsSeparator { get; set; }

        internal MenueItemInfo CreateCopy()
        {
            return new MenueItemInfo()
            {
                Index = Index,
                Path = Path,
                Name = Name,
                BackColor = BackColor,
                TextColor = TextColor,
                IsSeparator = IsSeparator,
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SideToolbars
{
    public class ToolbarPanel : Panel
    {
        public int ExpandSize { get; set; } = 200;
        public int CollapseSize { get; set; }

        public void Expand()
        {
            if (!IsExpanded)
            {
                new ControlAnimation(this, ExpandSize).Start();
            }
        }

        public void Collapse()
        {
            if (!IsCollapsed)
            {
                new ControlAnimation(this, CollapseSize).Start();
            }
        }

        public bool IsCollapsed
        {
            get
            {
                return !IsExpanded;
            }
        }

        public bool IsExpanded
        {
            get
            {
                int currentValue = 0;
                switch (this.Dock)
                {
                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        currentValue = this.Height;
                        break;

                    case DockStyle.Left:
                    case DockStyle.Right:
                        currentValue = this.Width;
                        break;

                    case DockStyle.Fill:
                    case DockStyle.None:
                    default:
                        break;
                }

                return currentValue > CollapseSize;
            }
        }
    }
}

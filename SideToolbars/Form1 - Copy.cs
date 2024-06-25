using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SideToolbars
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            ShowToolbars(GetScreenContainsCursor());
        }

        private Screen GetScreenContainsCursor()
        {
            foreach (var screen in Screen.AllScreens)
            {
                if (screen.Bounds.Contains(MousePosition))
                {
                    return screen;
                }
            }

            return Screen.PrimaryScreen;
        }

        private void ShowToolbars(Screen screen)
        {
            this.Show();
            this.Location = screen.WorkingArea.Location;
            this.Size = screen.WorkingArea.Size;
        }

        private void tmrAutoExpand_Tick(object sender, EventArgs e)
        {
            Screen screen = GetScreenContainsCursor();

            var topRect = new Rectangle(screen.Bounds.Left, screen.Bounds.Top, screen.Bounds.Width, screen.Bounds.Height / 3);

            var leftRect = new Rectangle(screen.Bounds.Left, topRect.Bottom, screen.Bounds.Width / 2, screen.Bounds.Height / 3);
            var leftRectExact = new Rectangle(screen.Bounds.Left, screen.Bounds.Height / 4, screen.Bounds.Width / 4, (screen.Bounds.Height / 4)*3);

            var rightRect = new Rectangle(screen.Bounds.Width / 2, topRect.Bottom, screen.Bounds.Width / 2, screen.Bounds.Height / 3);
            var rightRectExact = new Rectangle((screen.Bounds.Width / 4) * 2, topRect.Bottom, screen.Bounds.Width / 4, screen.Bounds.Height / 3);

            var downRect = new Rectangle(screen.Bounds.Left, screen.Bounds.Height - screen.Bounds.Height / 3, screen.Bounds.Width, screen.Bounds.Height / 3);

            var topRectedPointed = topRect.Contains(MousePosition);
            var leftRectPointed = leftRect.Contains(MousePosition);
            var leftRectExactPointed = leftRectExact.Contains(MousePosition);
            var rightRectPointed = rightRect.Contains(MousePosition);
            var rightRectExactPointed = rightRectExact.Contains(MousePosition);
            var downRectPointed = downRect.Contains(MousePosition);

            topRectedPointed = (topRectedPointed && !leftRectPointed && !rightRectPointed && !leftRectExactPointed && !rightRectExactPointed);
            downRectPointed = (downRectPointed && !leftRectPointed && !rightRectPointed && !leftRectExactPointed && !rightRectExactPointed);
            leftRectPointed = (leftRectPointed && !topRectedPointed && !downRectPointed) || leftRectExactPointed;
            rightRectPointed = (rightRectPointed && !topRectedPointed && !downRectPointed) || rightRectExactPointed;

            if (topRectedPointed)
            {
                downPanel.Collapse();
                leftPanel.Collapse();
                rightPanel.Collapse();
                topPanel.Expand();
            }

            if (rightRectPointed)
            {
                topPanel.Collapse();
                downPanel.Collapse();
                leftPanel.Collapse();
                rightPanel.Expand();
            }

            if (downRectPointed)
            {
                topPanel.Collapse();
                rightPanel.Collapse();
                leftPanel.Collapse();
                downPanel.Expand();
            }

            if (leftRectPointed)
            {
                topPanel.Collapse();
                downPanel.Collapse();
                rightPanel.Collapse();
                leftPanel.Expand();
            }
        }
    }
}

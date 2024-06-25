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
        private Triangle lSection;
        private Triangle rSection;
        private Triangle tSection;
        private Triangle dSection;

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

            lSection = new Triangle(
                screen.Bounds.X, screen.Bounds.Y + screen.Bounds.Height / 10,
                screen.Bounds.Left + screen.Bounds.Width / 2, screen.Bounds.Height / 2,
                screen.Bounds.Left, screen.Bounds.Height - (screen.Bounds.Height / 10));

            rSection = new Triangle(
                screen.Bounds.Right, screen.Bounds.Y + screen.Bounds.Height / 10,
                screen.Bounds.Left + screen.Bounds.Width / 2, screen.Bounds.Height / 2,
                screen.Bounds.Right, screen.Bounds.Height - (screen.Bounds.Height / 10));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void tmrAutoExpand_Tick(object sender, EventArgs e)
        {
            Screen screen = GetScreenContainsCursor();

            var x = MousePosition.X;
            var y = MousePosition.Y;
            var lx = x - screen.Bounds.X;
            var ly = y - screen.Bounds.Y;

            var leftRectPointed = lSection.HitTest(new Point(lx, ly));
            var rightRectPointed = rSection.HitTest(new Point(lx, ly));
            var topRectPointed = ly < screen.Bounds.Height / 2 && !leftRectPointed && !rightRectPointed;
            var downRectPointed = ly > screen.Bounds.Height / 2 && !leftRectPointed && !rightRectPointed;

            if (topRectPointed)
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

            Invalidate();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Hide();
        }
    }

    public class Triangle
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int X3 { get; set; }
        public int Y3 { get; set; }


        public Point P1
        {
            get
            {
                return new Point(X1, Y1);
            }
        }

        public Point P2
        {
            get
            {
                return new Point(X2, Y2);
            }
        }

        public Point P3
        {
            get
            {
                return new Point(X3, Y3);
            }
        }

        public Triangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            this.X1 = x1;
            this.Y1 = y1;

            this.X2 = x2;
            this.Y2 = y2;

            this.X3 = x3;
            this.Y3 = y3;

        }


        float sign(Point p1, Point p2, Point p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }

        public bool HitTest(Point pt)
        {
            float d1, d2, d3;
            bool has_neg, has_pos;

            d1 = sign(pt, P1, P2);
            d2 = sign(pt, P2, P3);
            d3 = sign(pt, P3, P1);

            has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(has_neg && has_pos);
        }

        public bool HitTest(int x, int y)
        {
            double a = ((Y2 - Y3) * (x - X3) + (X3 - X2) * (y - Y3)) / ((Y2 - Y3) * (X1 - X3) + (X3 - X2) * (Y1 - Y3));
            double b = ((Y3 - Y1) * (x - X3) + (X1 - X3) * (y - Y3)) / ((Y2 - Y3) * (X1 - X3) + (X3 - X2) * (Y1 - Y3));
            double c = 1 - a - b;

            if (a == 0 || b == 0 || c == 0)
            {
                return true;
            }
            else if (a >= 0 && a <= 1 && b >= 0 && b <= 1 && c >= 0 && c <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

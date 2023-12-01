using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadAnimationControl
{
    public partial class UserControl1 : UserControl
    {
        private List<Shape> shapes;

        public UserControl1()
        {
            InitializeComponent();
            shapes = new List<Shape>();
            shapes.Add(new Shape() { Size = lbl1.Size, Location = lbl1.Location });
            shapes.Add(new Shape() { Size = lbl2.Size, Location = lbl2.Location });
            shapes.Add(new Shape() { Size = lbl3.Size, Location = lbl3.Location });
            shapes.Add(new Shape() { Size = lbl4.Size, Location = lbl4.Location });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < shapes.Count; i++)
            {
                shapes[i].Animate();
            }
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            for (int i = 0; i < shapes.Count; i++)
            {
                e.Graphics.FillEllipse(Brushes.DodgerBlue, new RectangleF(shapes[i].Location, shapes[i].Size));
            }
        }
    }

    public class Shape
    {
        public Size Size;
        public Point Location;
        private bool mustBeSmaller = true;

        public void Animate()
        {
            if (mustBeSmaller)
            {
                if (!Smaller())
                {
                    mustBeSmaller = false;
                }
            }
            else
            {
                if (!Larger())
                {
                    mustBeSmaller = true;
                }
            }
        }

        private bool Smaller()
        {
            if (Size.Width > 0 && Size.Height > 0)
            {
                Size = new Size(Size.Width - 2, Size.Height - 2);
                Location = new Point(Location.X + 1, Location.Y + 1);
                return true;
            }

            return false;
        }

        private bool Larger()
        {
            if (Size.Width < 50 && Size.Height < 50)
            {
                Size = new Size(Size.Width + 2, Size.Height + 2);
                Location = new Point(Location.X - 1, Location.Y - 1);
                return true;
            }
            return false;
        }

    }

}

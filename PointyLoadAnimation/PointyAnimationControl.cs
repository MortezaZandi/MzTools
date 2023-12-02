using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointyLoadAnimation
{
    public partial class PointyAnimationControl : UserControl
    {
        public int ElementsCount
        {
            get
            {
                return this.elementCount;
            }
            set
            {
                if (value <= 5 && value != elementCount)
                {
                    this.elementCount = value;
                    CreateAnimationShapes();
                }
            }
        }

        public Color ElementColor1 { get { return color1; } set { this.color1 = value; CreateAnimationShapes(); } }
        public Color ElementColor2 { get { return color2; } set { this.color2 = value; CreateAnimationShapes(); } }
        public Color ElementColor3 { get { return color3; } set { this.color3 = value; CreateAnimationShapes(); } }
        public Color ElementColor4 { get { return color4; } set { this.color4 = value; CreateAnimationShapes(); } }
        public Color ElementColor5 { get { return color5; } set { this.color5 = value; CreateAnimationShapes(); } }
        public int Speed
        {
            get
            {
                return timer.Interval;
            }
            set
            {
                timer.Interval = value;
            }
        }

        private List<Color> colors;
        private Timer timer = new Timer();
        private List<Shape> shapes = new List<Shape>();
        private int elementCount = 3;
        private Color color1 = Color.FromArgb(67, 85, 219);
        private Color color2 = Color.FromArgb(67, 85, 219);
        private Color color3 = Color.FromArgb(67, 85, 219);
        private Color color4 = Color.FromArgb(67, 85, 219);
        private Color color5 = Color.FromArgb(67, 85, 219);

        public PointyAnimationControl()
        {
            InitializeComponent();

            colors = new List<Color>
            {
               color1, color2, color3, color4,color5,
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CreateAnimationShapes();
            StartAnimation();
        }

        private void StartAnimation()
        {
            timer.Interval = 35;
            timer.Enabled = true;
            timer.Tick += (s, e) =>
            {
                foreach (var shape in shapes)
                {
                    shape.Animate();
                }

                this.Invalidate();
            };
        }

        private void CreateAnimationShapes()
        {
            shapes = new List<Shape>();
            int maxSize = this.Width / ElementsCount;
            int minSize = maxSize / ElementsCount;
            for (int i = 0; i < ElementsCount; i++)
            {
                float x = (i * maxSize) + maxSize / 2;
                float y = this.Height / 2;

                Shape shape = new Shape();
                shape.Color = colors[i];
                shape.MaxSize = new Size(maxSize, maxSize);
                shape.Size = new Size(i * minSize, i * minSize);
                shape.Location = new Point((int)(x - shape.Size.Width / 2f), (int)(y - shape.Size.Height / 2f));
                shapes.Add(shape);
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            foreach (var shape in shapes)
            {
                shape.Draw(e.Graphics);
            }
        }
    }

    public class Shape
    {
        public Size Size;
        public Point Location;
        private bool mustBeSmaller = true;
        private Brush fillBrush;

        public Color Color { get; set; }
        public Size MaxSize { get; set; }

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

            if (Size.Width < 0)
            {
                Size = new Size(0, Size.Height);
            }

            if (Size.Height < 0)
            {
                Size = new Size(Size.Width, 0);
            }

            return false;
        }

        private bool Larger()
        {
            if (Size.Width < MaxSize.Width && Size.Height < MaxSize.Height)
            {
                Size = new Size(Size.Width + 2, Size.Height + 2);
                Location = new Point(Location.X - 1, Location.Y - 1);
                return true;
            }


            if (Size.Width > MaxSize.Width)
            {
                Size = new Size(MaxSize.Width, Size.Height);
            }

            if (Size.Height < MaxSize.Height)
            {
                Size = new Size(Size.Width, MaxSize.Height);
            }

            return false;
        }

        public void Draw(Graphics g)
        {
            if (fillBrush == null)
            {
                fillBrush = new SolidBrush(Color);
            }

            g.FillEllipse(fillBrush, new RectangleF(Location, Size));
        }

    }

}

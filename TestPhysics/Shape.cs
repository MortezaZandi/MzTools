using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPhysics
{
    public abstract class Shape
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color BackColor { get; set; }
        public Color BorderColor { get; set; } = Color.Gray;
        public float BorderSize { get; set; } = 1;

        public RectangleF Rect
        {
            get
            {
                return new RectangleF(X, Y, Width, Height);
            }
        }

        public abstract void Draw(Graphics g);

    }

    public class Circle : Shape
    {
        public float Radius { get; set; }

        public override void Draw(Graphics g)
        {
            g.FillEllipse(new SolidBrush(BackColor), Rect);
            g.DrawEllipse(new Pen(BorderColor, BorderSize), Rect);

            //g.DrawRectangle(Pens.LightGray, Rect.X, Rect.Y, Rect.Width, Rect.Height);
        }
    }

    public abstract class Colider
    {
        public abstract bool IsColideWith(Colider other);
    }

    public class CircleColider : Colider
    {
        private float x;
        private float y;
        private float width;
        private float height;

        public CircleColider(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }




        public override bool IsColideWith(Colider other)
        {

        }
    }
}

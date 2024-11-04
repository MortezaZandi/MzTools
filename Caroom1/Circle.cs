using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caroom1
{
    public class Circle
    {
        public float Radius { get; set; }
        public float CenterX { get; set; }
        public float CenterY { get; set; }

        public RectangleF Rect
        {
            get
            {
                return new RectangleF
                {
                    X = CenterX - Radius,
                    Y = CenterY - Radius,
                    Width = Radius * 2,
                    Height = Radius * 2,
                };
            }
        }

        public Circle Clone(float x, float y)
        {
            return new Circle
            {
                CenterX = x,
                CenterY = y,
                Radius = Radius,
            };
        }

        public bool IsCollideWith(Circle other)
        {
            var radius = this.Radius + other.Radius;
            var deltaX = this.CenterX - other.CenterX;
            var deltaY = this.CenterY - other.CenterY;
            return deltaX * deltaX + deltaY * deltaY <= radius * radius;
        }
    }
}

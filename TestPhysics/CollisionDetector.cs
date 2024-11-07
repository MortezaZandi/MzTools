using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPhysics
{
    public class CollisionDetector
    {
        private readonly List<Shape> shapes;
        public CollisionDetector(List<Shape> shapes)
        {
            this.shapes = shapes;
        }

        public void Update()
        {
            foreach (var shape in shapes)
            {
                foreach (var other in shapes)
                {
                    if (shape != other)
                    {
                        //check
                    }
                }
            }
        }

        internal bool CanMove(Shape shapeToMove, int x, int y)
        {
            var newShapeRect = new RectangleF(x, y, shapeToMove.Width, shapeToMove.Height);

            foreach (var other in shapes)
            {
                if (other != shapeToMove)
                {
                    if (newShapeRect.IntersectsWith(other.Rect))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}

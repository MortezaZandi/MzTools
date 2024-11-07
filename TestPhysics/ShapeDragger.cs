using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPhysics
{
    internal class ShapeDragger
    {
        private readonly Control parent;
        private readonly List<Shape> shapes;
        private readonly CollisionDetector collisionDetector;
        private Shape clickedShape;
        private Point clickedLocation;
        public ShapeDragger(Control parent, List<Shape> shapes, CollisionDetector collisionDetector)
        {
            this.parent = parent;
            this.shapes = shapes;
            this.collisionDetector = collisionDetector;

            this.parent.MouseDown += Parent_MouseDown;
            this.parent.MouseMove += Parent_MouseMove;
        }

        private void Parent_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && clickedShape != null)
            {
                var newX = e.X - clickedLocation.X;
                var newY = e.Y - clickedLocation.Y;

                if (collisionDetector.CanMove(clickedShape, newX, newY))
                {
                    clickedShape.X = newX;
                    clickedShape.Y = newY;
                    collisionDetector.Update();
                    parent.Invalidate();
                }
            }
        }

        private void Parent_MouseDown(object sender, MouseEventArgs e)
        {
            clickedShape = FindShapeByPoint(e.X, e.Y);

            if (clickedShape != null)
            {
                clickedLocation = new Point(e.Location.X - clickedShape.X, e.Location.Y - clickedShape.Y);
            }
        }

        private Shape FindShapeByPoint(int x, int y)
        {
            foreach (var shape in shapes)
            {
                if (shape.Rect.Contains(x, y)) return shape;
            }

            return null;
        }
    }
}

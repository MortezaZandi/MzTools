using System.Drawing;
using System.Linq;

namespace MBase.UI.WinForm.Shapes
{
    public class TriangleShape : Shape
    {
        public Point[] Points { get; set; }

        public TriangleShape(Point[] points)
        {
            Points = points.ToArray(); // Create a copy of the points
        }

        public override void Draw(Graphics g, Pen pen)
        {
            g.DrawPolygon(pen, Points);
        }

        public override bool Contains(Point point)
        {
            // Simple bounding box check for now
            var bounds = new Rectangle(
                Points.Min(p => p.X),
                Points.Min(p => p.Y),
                Points.Max(p => p.X) - Points.Min(p => p.X),
                Points.Max(p => p.Y) - Points.Min(p => p.Y)
            );
            return bounds.Contains(point);
        }

        public override void Move(int deltaX, int deltaY)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i] = new Point(Points[i].X + deltaX, Points[i].Y + deltaY);
            }
        }
    }
} 
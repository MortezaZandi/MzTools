using System.Drawing;

namespace MBase.UI.WinForm.Shapes
{
    public class RectangleShape : Shape
    {
        public Rectangle Bounds { get; set; }

        public RectangleShape(Rectangle bounds)
        {
            Bounds = bounds;
        }

        public override void Draw(Graphics g, Pen pen)
        {
            g.DrawRectangle(pen, Bounds);
        }

        public override bool Contains(Point point)
        {
            return Bounds.Contains(point);
        }

        public override void Move(int deltaX, int deltaY)
        {
            Bounds = new Rectangle(
                Bounds.X + deltaX,
                Bounds.Y + deltaY,
                Bounds.Width,
                Bounds.Height
            );
        }
    }
} 
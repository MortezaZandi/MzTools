using System.Drawing;

namespace MBase.UI.WinForm.Shapes
{
    public abstract class Shape
    {
        public abstract void Draw(Graphics g, Pen pen);
        public abstract bool Contains(Point point);
        public abstract void Move(int deltaX, int deltaY);
    }
} 
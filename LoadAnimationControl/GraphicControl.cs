using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace howto_point_segment_distance
{
    public abstract class GraphicControl : Control
    {
        private Bitmap _bmpBackBuffer;

        public GraphicControl() : base()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            //this.SetStyle(ControlStyles.UserPaint, true);

            AutoSize = false;
            Text = string.Empty;
        }

        protected void RebuildUI()
        {
            _bmpBackBuffer = null;
            this.Refresh();
        }

        protected void RefreshUI()
        {
            using (Graphics g = CreateGraphics())
            {
                OnPaint(new PaintEventArgs(g, DisplayRectangle));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            if (_bmpBackBuffer == null)
            {
                Draw(ref _bmpBackBuffer);
            }

            if (_bmpBackBuffer != null)
            {
                e.Graphics.DrawImage(_bmpBackBuffer, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
            }
        }

        private void Draw(ref Bitmap bmp)
        {
            if (bmp == null)
            {
                bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            }

            using (Graphics gr = Graphics.FromImage(bmp))
            {
                DrawGraphics(gr, new Rectangle(0, 0, this.Width, this.Height));
            }
        }

        protected virtual void DrawGraphics(Graphics gr, Rectangle rectangle)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointyLoadAnimation
{
    public class SPanel : Panel
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.FillRoundedRectangle(new SolidBrush(Color.White), 10, 10, this.Width - 40, this.Height - 60, 4);
            SolidBrush brush = new SolidBrush(
                Color.White
                );
            g.FillRoundedRectangle(brush, 12, 12, this.Width - 44, this.Height - 64, 4);
            g.DrawRoundedRectangle(new Pen(ControlPaint.Light(Color.White, 0.00f)), 12, 12, this.Width - 44, this.Height - 64, 4);
            g.FillRoundedRectangle(new SolidBrush(Color.White), 12, 12 + ((this.Height - 64) / 2), this.Width - 44, (this.Height - 64) / 2, 4);
        }
    }
}

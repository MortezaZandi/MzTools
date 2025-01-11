using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPrinter

{
    public class RoundLabel : Label
    {
        private Color fillColor = Color.FromArgb(20, 22, 25);
        private Color borderColor = Color.DarkGray;
        private float radius = 3;
        private Padding offset = new Padding(5);
        private float borderSize = 1;
        private Brush fillBrush;
        private Brush borderBrush;
        private Pen borderPen;

        public RoundLabel()
        {
            fillBrush = new SolidBrush(fillColor);
            borderPen = new Pen(borderColor);
            borderBrush = new SolidBrush(borderColor);
        }

        public Color FillColor { get => fillColor; set { fillColor = value; fillBrush = new SolidBrush(fillColor); Redraw(); } }
        public Color BorderColor { get => borderColor; set { borderColor = value; borderPen = new Pen(borderColor); borderBrush = new SolidBrush(borderColor); Redraw(); } }
        public float Radius { get => radius; set { radius = value; Redraw(); } }
        public Padding Offset { get => offset; set { offset = value; Redraw(); } }
        public float BorderSize { get => borderSize; set { borderSize = value; Redraw(); } }

        private void Redraw()
        {
            if (IsHandleCreated)
            {
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            g.FillRoundedRectangle(borderBrush,
                Offset.Left,
                Offset.Top,
                this.Width - (Offset.Left + Offset.Right),
                this.Height - (Offset.Top + Offset.Bottom), radius);


            g.FillRoundedRectangle(fillBrush,
                Offset.Left + borderSize,
                Offset.Top + borderSize,
                this.Width - (Offset.Left + Offset.Right + borderSize + borderSize),
                this.Height - (Offset.Top + Offset.Bottom + borderSize + borderSize), radius);

            base.OnPaint(e);
        }
    }
}

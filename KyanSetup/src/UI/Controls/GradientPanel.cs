using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KyanSetup
{
    public class GradientPanel : Panel
    {
        public GradientPanel()
        {
            //SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private Color color1 = Color.WhiteSmoke;
        private Color color2 = Color.White;
        private LinearGradientMode gradientDirection = LinearGradientMode.BackwardDiagonal;
        private LinearGradientBrush brush;
        private Pen borderPen;

        private Color borderColor;

        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                borderPen = new Pen(borderColor);
                Invalidate();
            }
        }


        private bool showLeftBorder;
        private bool showRightBorder;
        private bool showTopBorder;
        private bool showBottomBorder;



        public Color GradientColor1
        {
            get => color1;
            set
            {
                color1 = value;
                brush = null;
                Invalidate();
            }
        }
        public Color GradientColor2
        {
            get => color2;
            set
            {
                color2 = value;
                brush = null;
                Invalidate();
            }
        }

        public LinearGradientMode GradientDirection
        {
            get
            {
                return this.gradientDirection;
            }
            set
            {
                this.gradientDirection = value;
                brush = null;
                Invalidate();
            }
        }

        public bool ShowLeftBorder { get => showLeftBorder; set { showLeftBorder = value; Invalidate(); } }
        public bool ShowRightBorder { get => showRightBorder; set { showRightBorder = value; Invalidate(); } }
        public bool ShowTopBorder { get => showTopBorder; set { showTopBorder = value; Invalidate(); } }
        public bool ShowBottomBorder { get => showBottomBorder; set { showBottomBorder = value; Invalidate(); } }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            brush = null;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (brush == null)
            {
                brush = CreateBrush(0, 0, Width, Height);
            }

            e.Graphics.FillRectangle(this.brush, e.ClipRectangle);


            if (showLeftBorder && showBottomBorder && showRightBorder && showTopBorder)
            {
                e.Graphics.DrawRectangle(borderPen, 1, 1, Width - 2, Height - 2);
            }
            else
            {
                if (showLeftBorder)
                {
                    e.Graphics.DrawLine(borderPen, 1, 0, 1, Height);
                }

                if (showTopBorder)
                {
                    e.Graphics.DrawLine(borderPen, 1, 1, Width, 1);
                }

                if (showRightBorder)
                {
                    e.Graphics.DrawLine(borderPen, Width - 1, 1, Width - 1, Height);
                }

                if (showBottomBorder)
                {
                    e.Graphics.DrawLine(borderPen, 1, Height - 1, Width - 1, Height - 1);
                }
            }
        }

        public LinearGradientBrush CreateBrush(int x, int y, int w, int h, Color? c1 = null, Color? c2 = null, bool reverse = false)
        {
            if (!c1.HasValue) c1 = color1;
            if (!c2.HasValue) c2 = color2;

            if (reverse)
            {
                return new LinearGradientBrush(new Rectangle(x, y, w, h + 5), c2.Value, c1.Value, gradientDirection);
            }
            else
            {
                return new LinearGradientBrush(new Rectangle(x, y, w, h + 5), c1.Value, c2.Value, gradientDirection);
            }
        }
    }
}

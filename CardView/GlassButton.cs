using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace CustomControls
{
    public class GlassButton : Button
    {
        private Color gradientTop = Color.FromArgb(200, 100, 200, 255);
        private Color gradientBottom = Color.FromArgb(200, 50, 100, 255);
        private Color borderColor = Color.FromArgb(180, 180, 180);
        private int borderWidth = 1;
        private int cornerRadius = 8;
        private bool isHovered = false;
        private bool isPressed = false;

        [Category("Appearance")]
        public Color GradientTopColor
        {
            get => gradientTop;
            set
            {
                gradientTop = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public Color GradientBottomColor
        {
            get => gradientBottom;
            set
            {
                gradientBottom = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public int BorderWidth
        {
            get => borderWidth;
            set
            {
                borderWidth = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public int CornerRadius
        {
            get => cornerRadius;
            set
            {
                cornerRadius = value;
                Invalidate();
            }
        }

        public GlassButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.SupportsTransparentBackColor |
                    ControlStyles.UserPaint, true);

            Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            BackColor = Color.Transparent;
            ForeColor = Color.White;
            Size = new Size(120, 40);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Adjust rectangle to account for border width
            Rectangle rect = new Rectangle(
                borderWidth / 2,
                borderWidth / 2,
                Width - 1 - borderWidth,
                Height - 1 - borderWidth);

            // Adjust colors based on button state
            var topColor = gradientTop;
            var bottomColor = gradientBottom;

            if (isPressed)
            {
                topColor = ControlPaint.Dark(gradientTop, 10);
                bottomColor = ControlPaint.Dark(gradientBottom, 10);
            }
            else if (isHovered)
            {
                topColor = ControlPaint.Light(gradientTop, 0.5f);
                bottomColor = ControlPaint.Light(gradientBottom, 0.5f);
            }

            using (var path = GetRoundedRectangle(rect, cornerRadius))
            {
                // Draw gradient background
                using (var brush = new LinearGradientBrush(rect, topColor, bottomColor, 90f))
                {
                    g.FillPath(brush, path);
                }

                // Draw glass reflection
                using (var glassBrush = new LinearGradientBrush(
                    rect,
                    Color.FromArgb(isHovered ? 180 : 120, Color.White),
                    Color.FromArgb(isHovered ? 90 : 30, Color.White),
                    90f))
                {
                    g.FillPath(glassBrush, path);
                }

                // Draw border
                using (var pen = new Pen(
                    isHovered ? ControlPaint.Light(borderColor) : borderColor,
                    borderWidth))
                {
                    pen.Alignment = PenAlignment.Center;
                    g.DrawPath(pen, path);
                }
            }

            // Draw text
            TextRenderer.DrawText(g, Text, Font,
                ClientRectangle,
                ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            
            if (radius > 0)
            {
                float diameter = radius * 2;
                var arcRect = new RectangleF(rect.Location, new SizeF(diameter, diameter));

                // Top left
                path.AddArc(arcRect, 180, 90);

                // Top edge
                path.AddLine(rect.Left + radius, rect.Top, rect.Right - radius, rect.Top);

                // Top right
                arcRect.X = rect.Right - diameter;
                path.AddArc(arcRect, 270, 90);

                // Right edge
                path.AddLine(rect.Right, rect.Top + radius, rect.Right, rect.Bottom - radius);

                // Bottom right
                arcRect.Y = rect.Bottom - diameter;
                path.AddArc(arcRect, 0, 90);

                // Bottom edge
                path.AddLine(rect.Right - radius, rect.Bottom, rect.Left + radius, rect.Bottom);

                // Bottom left
                arcRect.X = rect.Left;
                path.AddArc(arcRect, 90, 90);

                // Left edge
                path.AddLine(rect.Left, rect.Bottom - radius, rect.Left, rect.Top + radius);
            }
            else
            {
                path.AddRectangle(rect);
            }

            path.CloseFigure();
            return path;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isHovered = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            isPressed = true;
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isPressed = false;
            Invalidate();
            base.OnMouseUp(e);
        }
    }
} 
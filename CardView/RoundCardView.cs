using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace CustomControls
{
    public partial class RoundCardView : Panel
    {
        private int borderRadius = 15;
        private int shadowSize = 3;
        private Color shadowColor = Color.DarkGray;
        private bool drawShadow = true;

        [Category("Appearance")]
        public int BorderRadius
        {
            get => borderRadius;
            set
            {
                borderRadius = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public int ShadowSize
        {
            get => shadowSize;
            set
            {
                shadowSize = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public Color ShadowColor
        {
            get => shadowColor;
            set
            {
                shadowColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public bool DrawShadow
        {
            get => drawShadow;
            set
            {
                drawShadow = value;
                Invalidate();
            }
        }

        public RoundCardView()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.SupportsTransparentBackColor |
                    ControlStyles.UserPaint, true);
            BackColor = Color.White;
            Padding = new Padding(shadowSize);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var shadowRect = new Rectangle(shadowSize, 
                                        shadowSize, 
                                        Width - (shadowSize * 2), 
                                        Height - (shadowSize * 2));

            if (drawShadow)
            {
                // Draw main shadow
                using (var path = GetRoundPath(shadowRect, borderRadius))
                {
                    using (var brush = new SolidBrush(shadowColor))
                    {
                        g.TranslateTransform(shadowSize / 2f, shadowSize / 2f);
                        g.FillPath(brush, path);
                        g.TranslateTransform(-shadowSize / 2f, -shadowSize / 2f);
                    }
                }

                // Draw inner shadows for left and top edges
                var cardRect = new Rectangle(0, 0, Width - shadowSize, Height - shadowSize);
                using (var path = GetRoundPath(cardRect, borderRadius))
                {
                    // Draw first layer of inner shadow (stronger)
                    using (var pen = new Pen(Color.FromArgb(120, shadowColor), 2f))
                    {
                        g.DrawPath(pen, path);
                    }

                    // Draw second layer of inner shadow (slightly offset)
                    cardRect.Offset(2, 2);
                    using (var innerPath = GetRoundPath(cardRect, borderRadius))
                    using (var pen = new Pen(Color.FromArgb(60, shadowColor), 1.5f))
                    {
                        g.DrawPath(pen, innerPath);
                    }

                    // Draw third layer for extra depth
                    cardRect.Offset(1, 1);
                    using (var innerPath = GetRoundPath(cardRect, borderRadius))
                    using (var pen = new Pen(Color.FromArgb(30, shadowColor), 1f))
                    {
                        g.DrawPath(pen, innerPath);
                    }
                }
            }

            // Draw main card
            using (var path = GetRoundPath(new Rectangle(0, 0, Width - shadowSize, 
                                         Height - shadowSize), borderRadius))
            {
                using (var brush = new SolidBrush(BackColor))
                {
                    g.FillPath(brush, path);
                }
            }
        }

        private GraphicsPath GetRoundPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            float r = radius;
            float d = r * 2;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Width - d + rect.X, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Width - d + rect.X, rect.Height - d + rect.Y, d, d, 0, 90);
            path.AddArc(rect.X, rect.Height - d + rect.Y, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
} 
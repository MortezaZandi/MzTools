using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace NodeEditor
{
    public class FunctionNodeControl : Control
    {
        private const int HeaderHeight = 25;
        private const int PortHeight = 20;
        private const int PortSpacing = 5;
        private const int PortCircleRadius = 5;
        
        private IFunction function;
        private bool isDragging;
        private Point dragOffset;
        private Rectangle headerRect;
        private Dictionary<Rectangle, IFunctionPort> portRects = new Dictionary<Rectangle, IFunctionPort>();

        public IFunction Function
        {
            get => function;
            set
            {
                function = value;
                UpdateSize();
                Invalidate();
            }
        }

        public event EventHandler<IFunctionPort> PortClicked;

        // Add parameterless constructor for designer
        public FunctionNodeControl()
        {
            InitializeControl();
        }

        // Keep existing constructor for runtime
        public FunctionNodeControl(IFunction function) : this()
        {
            this.function = function;
            UpdateSize();
        }

        private void InitializeControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.SupportsTransparentBackColor |
                    ControlStyles.UserPaint, true);
            
            BackColor = Color.FromArgb(64, 64, 64);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9F);
            Size = new Size(180, HeaderHeight); // Default size when no function is set
        }

        private void UpdateSize()
        {
            if (function == null)
            {
                Size = new Size(180, HeaderHeight);
                headerRect = new Rectangle(0, 0, Width, HeaderHeight);
                return;
            }

            int width = 180;
            int height = HeaderHeight;
            
            height += Math.Max(
                (function.Inputs?.Count ?? 0) * (PortHeight + PortSpacing),
                (function.Outputs?.Count ?? 0) * (PortHeight + PortSpacing)
            );

            // Add extra space for child controls
            height += 40; // Adjust this value as needed

            Size = new Size(width, height);
            headerRect = new Rectangle(0, 0, width, HeaderHeight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw node background
            using (var brush = new SolidBrush(BackColor))
            {
                g.FillRectangle(brush, ClientRectangle);
            }

            // Draw header
            using (var brush = new SolidBrush(Color.FromArgb(80, 80, 80)))
            {
                g.FillRectangle(brush, headerRect);
            }

            // Draw node name
            using (var brush = new SolidBrush(ForeColor))
            {
                string name = function?.Name ?? "Node";
                g.DrawString(name, Font, brush, 
                    new PointF(5, (HeaderHeight - Font.Height) / 2));
            }

            if (function == null) return;

            portRects.Clear();

            // Draw input ports
            int y = HeaderHeight + PortSpacing;
            foreach (var input in function.Inputs.Values)
            {
                DrawPort(g, input, true, y);
                y += PortHeight + PortSpacing;
            }

            // Draw output ports
            y = HeaderHeight + PortSpacing;
            foreach (var output in function.Outputs.Values)
            {
                DrawPort(g, output, false, y);
                y += PortHeight + PortSpacing;
            }
        }

        private void DrawPort(Graphics g, IFunctionPort port, bool isInput, int y)
        {
            // Adjust port position
            int x = isInput ? 0 : Width - PortCircleRadius * 2;
            var portRect = new Rectangle(x, y, PortCircleRadius * 2, PortCircleRadius * 2);
            portRects[portRect] = port;

            // Draw port circle with border
            using (var brush = new SolidBrush(GetPortColor(port.DataType)))
            using (var pen = new Pen(Color.White, 1))
            {
                g.FillEllipse(brush, portRect);
                g.DrawEllipse(pen, portRect);
            }

            // Draw port name
            var textRect = new Rectangle(
                isInput ? PortCircleRadius * 2 + 5 : 5,
                y,
                Width - PortCircleRadius * 2 - 10,
                PortHeight);

            using (var brush = new SolidBrush(ForeColor))
            {
                var format = new StringFormat
                {
                    Alignment = isInput ? StringAlignment.Near : StringAlignment.Far,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(port.Name, Font, brush, textRect, format);
            }

            // Draw value
            if (port.Value != null)
            {
                string valueText;
                if (port.Value is float floatValue)
                {
                    valueText = floatValue.ToString("F2"); // Format to 2 decimal places
                }
                else
                {
                    valueText = port.Value.ToString();
                }

                var valueRect = new Rectangle(
                    isInput ? Width - 45 : 45,
                    y,
                    40,
                    PortHeight);

                using (var brush = new SolidBrush(Color.LightGray))
                {
                    g.DrawString(valueText, Font, brush, valueRect, 
                        new StringFormat { Alignment = StringAlignment.Far });
                }
            }
        }

        private Color GetPortColor(Type dataType)
        {
            if (dataType == typeof(float)) return Color.LightGreen;
            if (dataType == typeof(Color)) return Color.Pink;
            if (dataType == typeof(bool)) return Color.Yellow;
            if (dataType == typeof(string)) return Color.LightBlue;
            return Color.White;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                var port = portRects.FirstOrDefault(p => p.Key.Contains(e.Location));
                if (!port.Key.IsEmpty)
                {
                    PortClicked?.Invoke(this, port.Value);
                }
                else if (headerRect.Contains(e.Location))
                {
                    isDragging = true;
                    dragOffset = new Point(e.X, e.Y);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (isDragging)
            {
                Left = Left + (e.X - dragOffset.X);
                Top = Top + (e.Y - dragOffset.Y);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isDragging = false;
        }

        public Rectangle GetPortBounds(IFunctionPort port)
        {
            var portPair = portRects.FirstOrDefault(p => p.Value == port);
            if (!portPair.Key.IsEmpty)
            {
                // Convert to screen coordinates
                var bounds = portPair.Key;
                var screenPoint = this.PointToScreen(new Point(bounds.X, bounds.Y));
                bounds.Location = Parent.PointToClient(screenPoint);
                return bounds;
            }

            // If port not found, calculate its position
            int y = HeaderHeight + PortSpacing;
            
            if (port.IsInput)
            {
                foreach (var input in function.Inputs.Values)
                {
                    if (input == port)
                    {
                        var screenPoint = this.PointToScreen(new Point(0, y));
                        return new Rectangle(
                            Parent.PointToClient(screenPoint),
                            new Size(PortCircleRadius * 2, PortCircleRadius * 2));
                    }
                    y += PortHeight + PortSpacing;
                }
            }
            else
            {
                foreach (var output in function.Outputs.Values)
                {
                    if (output == port)
                    {
                        var screenPoint = this.PointToScreen(
                            new Point(Width - PortCircleRadius * 2, y));
                        return new Rectangle(
                            Parent.PointToClient(screenPoint),
                            new Size(PortCircleRadius * 2, PortCircleRadius * 2));
                    }
                    y += PortHeight + PortSpacing;
                }
            }

            return Rectangle.Empty;
        }
    }
} 
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomControls
{
    public class RotatingPictureBox : PictureBox
    {
        private float currentAngle = 0;
        private Timer rotationTimer;
        private bool isRotating = false;
        private float rotationSpeed = 1.0f;
        private bool clockwise = true;

        [Category("Behavior")]
        public bool IsRotating
        {
            get => isRotating;
            set
            {
                isRotating = value;
                if (isRotating)
                    StartRotation();
                else
                    StopRotation();
            }
        }

        [Category("Behavior")]
        public float RotationSpeed
        {
            get => rotationSpeed;
            set
            {
                rotationSpeed = Math.Max(0.1f, Math.Min(value, 20.0f)); // Limit between 0.1 and 20
                if (rotationTimer != null)
                {
                    rotationTimer.Interval = (int)(16); // Approximately 60 FPS
                }
            }
        }

        [Category("Behavior")]
        public bool Clockwise
        {
            get => clockwise;
            set
            {
                clockwise = value;
            }
        }

        [Category("Appearance")]
        public float CurrentAngle
        {
            get => currentAngle;
            set
            {
                currentAngle = value % 360;
                Invalidate();
            }
        }

        public RotatingPictureBox()
        {
            InitializeControl();
        }

        private void InitializeControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.SupportsTransparentBackColor |
                    ControlStyles.UserPaint, true);

            rotationTimer = new Timer();
            rotationTimer.Interval = 16; // Approximately 60 FPS
            rotationTimer.Tick += RotationTimer_Tick;
        }

        private void RotationTimer_Tick(object sender, EventArgs e)
        {
            float angleChange = rotationSpeed * (clockwise ? 1 : -1);
            CurrentAngle += angleChange;
        }

        public void StartRotation()
        {
            if (!rotationTimer.Enabled)
            {
                rotationTimer.Start();
            }
        }

        public void StopRotation()
        {
            if (rotationTimer.Enabled)
            {
                rotationTimer.Stop();
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            pe.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            if (Image == null) return;

            using (var matrix = new Matrix())
            {
                matrix.RotateAt(currentAngle, new PointF(Width / 2f, Height / 2f));
                pe.Graphics.Transform = matrix;

                // Calculate the centered position
                float imageAspect = (float)Image.Width / Image.Height;
                float controlAspect = (float)Width / Height;
                Rectangle imageRect;

                if (imageAspect > controlAspect)
                {
                    // Image is wider than control
                    int height = (int)(Width / imageAspect);
                    int y = (Height - height) / 2;
                    imageRect = new Rectangle(0, y, Width, height);
                }
                else
                {
                    // Image is taller than control
                    int width = (int)(Height * imageAspect);
                    int x = (Width - width) / 2;
                    imageRect = new Rectangle(x, 0, width, Height);
                }

                pe.Graphics.DrawImage(Image, imageRect);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                rotationTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
} 
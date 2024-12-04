using Autofac;
using MBase.Common.Definistions.Interfaces.AppServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MBase.UI.WinForm.Shapes;

namespace MBase.UI.WinForm
{
    public partial class Form1 : BaseDialog
    {
        private enum DrawingShape
        {
            Rectangle,
            Triangle
        }

        private readonly ISampleService sampleService;
        private readonly IFileService fileService;
        private bool isDrawing = false;
        private bool isDragging = false;
        private Point startPoint;
        private Rectangle currentRectangle;
        private Point[] trianglePoints = new Point[3];
        private Point dragOffset;
        private DrawingShape currentShape = DrawingShape.Rectangle;
        private bool isShapeComplete = false;
        private List<Shape> shapes = new List<Shape>();
        private Shape selectedShape;
        private const int SNAP_THRESHOLD = 10; // Pixels within which to show alignment
        private List<Point> guideLines = new List<Point>(); // Store guide line coordinates

        public Form1(ISampleService sampleService, IFileService fileService) : base()
        {
            InitializeComponent();

            this.sampleService = sampleService;
            this.fileService = fileService;

            // Enable double buffering to reduce flicker
            this.DoubleBuffered = true;

            // Add shape selection buttons
            AddShapeSelectionControls();
        }

        private void AddShapeSelectionControls()
        {
            Button rectangleButton = new Button
            {
                Text = "Rectangle",
                Location = new Point(10, 10),
                Size = new Size(80, 30)
            };
            rectangleButton.Click += (s, e) => currentShape = DrawingShape.Rectangle;

            Button triangleButton = new Button
            {
                Text = "Triangle",
                Location = new Point(100, 10),
                Size = new Size(80, 30)
            };
            triangleButton.Click += (s, e) => currentShape = DrawingShape.Triangle;

            this.Controls.Add(rectangleButton);
            this.Controls.Add(triangleButton);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (currentShape == DrawingShape.Rectangle)
                {
                    HandleRectangleMouseDown(e);
                }
                else if (currentShape == DrawingShape.Triangle)
                {
                    HandleTriangleMouseDown(e);
                }
            }
            base.OnMouseDown(e);
        }

        private void HandleRectangleMouseDown(MouseEventArgs e)
        {
            selectedShape = shapes.FirstOrDefault(s => s.Contains(e.Location));
            
            if (selectedShape != null && isShapeComplete)
            {
                isDragging = true;
                dragOffset = e.Location;
            }
            else
            {
                isDrawing = true;
                startPoint = e.Location;
                currentRectangle = new Rectangle();
                isShapeComplete = false;
            }
        }

        private void HandleTriangleMouseDown(MouseEventArgs e)
        {
            selectedShape = shapes.FirstOrDefault(s => s.Contains(e.Location));

            if (selectedShape != null && isShapeComplete)
            {
                isDragging = true;
                dragOffset = e.Location;
            }
            else if (!isDrawing)
            {
                isDrawing = true;
                startPoint = e.Location;
                trianglePoints = new Point[3];
                trianglePoints[0] = startPoint;
                trianglePoints[1] = startPoint;
                trianglePoints[2] = startPoint;
                isShapeComplete = false;
            }
        }

        private void CheckForAlignment(Point currentPoint)
        {
            guideLines.Clear();
            
            // Get all points from existing shapes
            var allPoints = new List<Point>();
            foreach (var shape in shapes)
            {
                if (shape is RectangleShape rectShape)
                {
                    // Add rectangle corners
                    allPoints.Add(new Point(rectShape.Bounds.Left, rectShape.Bounds.Top));
                    allPoints.Add(new Point(rectShape.Bounds.Right, rectShape.Bounds.Top));
                    allPoints.Add(new Point(rectShape.Bounds.Left, rectShape.Bounds.Bottom));
                    allPoints.Add(new Point(rectShape.Bounds.Right, rectShape.Bounds.Bottom));
                }
                else if (shape is TriangleShape triShape)
                {
                    // Add triangle points
                    allPoints.AddRange(triShape.Points);
                }
            }

            // Check for X alignment
            foreach (var point in allPoints)
            {
                if (Math.Abs(point.X - currentPoint.X) <= SNAP_THRESHOLD)
                {
                    // Add vertical guide line
                    guideLines.Add(new Point(point.X, 0));
                    guideLines.Add(new Point(point.X, this.ClientSize.Height));
                }
                if (Math.Abs(point.Y - currentPoint.Y) <= SNAP_THRESHOLD)
                {
                    // Add horizontal guide line
                    guideLines.Add(new Point(0, point.Y));
                    guideLines.Add(new Point(this.ClientSize.Width, point.Y));
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (isDragging && selectedShape != null)
            {
                int deltaX = e.Location.X - dragOffset.X;
                int deltaY = e.Location.Y - dragOffset.Y;
                selectedShape.Move(deltaX, deltaY);
                dragOffset = e.Location;

                // Check alignment for the shape's points
                if (selectedShape is RectangleShape rectShape)
                {
                    CheckForAlignment(new Point(rectShape.Bounds.Left, rectShape.Bounds.Top));
                    CheckForAlignment(new Point(rectShape.Bounds.Right, rectShape.Bounds.Top));
                    CheckForAlignment(new Point(rectShape.Bounds.Left, rectShape.Bounds.Bottom));
                    CheckForAlignment(new Point(rectShape.Bounds.Right, rectShape.Bounds.Bottom));
                }
                else if (selectedShape is TriangleShape triShape)
                {
                    foreach (var point in triShape.Points)
                    {
                        CheckForAlignment(point);
                    }
                }
                
                this.Invalidate();
            }
            else if (currentShape == DrawingShape.Rectangle && isDrawing)
            {
                int width = e.X - startPoint.X;
                int height = e.Y - startPoint.Y;

                int x = width < 0 ? e.X : startPoint.X;
                int y = height < 0 ? e.Y : startPoint.Y;
                width = Math.Abs(width);
                height = Math.Abs(height);

                currentRectangle = new Rectangle(x, y, width, height);
                CheckForAlignment(new Point(x, y));
                CheckForAlignment(new Point(x + width, y));
                CheckForAlignment(new Point(x, y + height));
                CheckForAlignment(new Point(x + width, y + height));
                
                this.Invalidate();
            }
            else if (currentShape == DrawingShape.Triangle && isDrawing)
            {
                double sideLength = Math.Sqrt(Math.Pow(e.X - startPoint.X, 2) + Math.Pow(e.Y - startPoint.Y, 2));
                double height = sideLength * Math.Sqrt(3) / 2;
                
                trianglePoints[0] = startPoint;
                
                double angle = Math.Atan2(e.Y - startPoint.Y, e.X - startPoint.X);
                
                trianglePoints[1] = new Point(
                    (int)(startPoint.X + sideLength * Math.Cos(angle)),
                    (int)(startPoint.Y + sideLength * Math.Sin(angle))
                );
                
                trianglePoints[2] = new Point(
                    (int)(startPoint.X + sideLength * Math.Cos(angle - 2 * Math.PI / 3)),
                    (int)(startPoint.Y + sideLength * Math.Sin(angle - 2 * Math.PI / 3))
                );

                foreach (var point in trianglePoints)
                {
                    CheckForAlignment(point);
                }
                
                this.Invalidate();
            }
            else if (shapes.Any(s => s.Contains(e.Location)))
            {
                this.Cursor = Cursors.SizeAll;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
            
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isDrawing)
                {
                    if (currentShape == DrawingShape.Rectangle && currentRectangle.Width > 0 && currentRectangle.Height > 0)
                    {
                        shapes.Add(new RectangleShape(currentRectangle));
                    }
                    else if (currentShape == DrawingShape.Triangle)
                    {
                        shapes.Add(new TriangleShape(trianglePoints));
                    }
                }

                isDrawing = false;
                isDragging = false;
                isShapeComplete = true;
                selectedShape = null;
                guideLines.Clear(); // Clear guide lines when mouse is released
                this.Invalidate();
            }
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            // Draw all completed shapes
            using (Pen shapePen = new Pen(Color.Black, 1))
            {
                foreach (var shape in shapes)
                {
                    shape.Draw(e.Graphics, shapePen);
                }

                // Draw the shape being currently drawn
                if (isDrawing)
                {
                    if (currentShape == DrawingShape.Rectangle && currentRectangle.Width > 0 && currentRectangle.Height > 0)
                    {
                        e.Graphics.DrawRectangle(shapePen, currentRectangle);
                    }
                    else if (currentShape == DrawingShape.Triangle)
                    {
                        e.Graphics.DrawPolygon(shapePen, trianglePoints);
                    }
                }
            }

            // Draw guide lines
            if (guideLines.Count > 0)
            {
                using (Pen guidePen = new Pen(Color.Blue, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
                {
                    for (int i = 0; i < guideLines.Count; i += 2)
                    {
                        e.Graphics.DrawLine(guidePen, guideLines[i], guideLines[i + 1]);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var file = @"C:\Tables.txt";

            var fileLine = fileService.ReadFirstLine(file);

            MessageBox.Show(this.sampleService.DecorateFileData(fileLine));
        }

        private int AddIntegers(int a, int b)
        {
            return a + b;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var newDialog = appContext.Container.Resolve<Form2>();
            newDialog.Show();
        }
    }

}

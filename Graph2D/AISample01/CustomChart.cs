using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class CustomChart : Control
{
    private List<PointF> dataPoints = new List<PointF>();
    
    public Color GridColor { get; set; } = Color.LightGray;
    public Color TextColor { get; set; } = Color.Black;
    public int BorderSize { get; set; } = 1;
    public Color BorderColor { get; set; } = Color.Black;
    public int GridLinesCount { get; set; } = 10;
    public string Title { get; set; } = "";
    public string XAxisLabel { get; set; } = "";
    public string YAxisLabel { get; set; } = "";

    public CustomChart()
    {
        this.DoubleBuffered = true;
    }

    public void SetData(List<PointF> points)
    {
        dataPoints = points;
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        // Draw title
        if (!string.IsNullOrEmpty(Title))
        {
            using (Font titleFont = new Font("Arial", 12, FontStyle.Bold))
            using (SolidBrush textBrush = new SolidBrush(TextColor))
            {
                SizeF titleSize = g.MeasureString(Title, titleFont);
                g.DrawString(Title, titleFont, textBrush, 
                    (Width - titleSize.Width) / 2, 5);
            }
        }

        // Draw border
        using (Pen borderPen = new Pen(BorderColor, BorderSize))
        {
            g.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
        }

        // Draw grid
        using (Pen gridPen = new Pen(GridColor, 1))
        {
            // Vertical lines
            for (int i = 1; i < GridLinesCount; i++)
            {
                float x = (Width * i) / GridLinesCount;
                g.DrawLine(gridPen, x, 0, x, Height);
            }

            // Horizontal lines
            for (int i = 1; i < GridLinesCount; i++)
            {
                float y = (Height * i) / GridLinesCount;
                g.DrawLine(gridPen, 0, y, Width, y);
            }
        }

        // Draw axis labels
        using (Font labelFont = new Font("Arial", 8))
        using (SolidBrush textBrush = new SolidBrush(TextColor))
        {
            // Y-axis label
            if (!string.IsNullOrEmpty(YAxisLabel))
            {
                g.TranslateTransform(15, Height / 2);
                g.RotateTransform(-90);
                g.DrawString(YAxisLabel, labelFont, textBrush, 0, 0);
                g.ResetTransform();
            }

            // X-axis label
            if (!string.IsNullOrEmpty(XAxisLabel))
            {
                g.DrawString(XAxisLabel, labelFont, textBrush,
                    Width / 2, Height - 20);
            }
        }

        // Draw data points and lines
        if (dataPoints.Count > 0)
        {
            using (Pen dataPen = new Pen(Color.Purple, 1))
            {
                // Find min and max values for scaling
                float minX = dataPoints.Min(p => p.X);
                float maxX = dataPoints.Max(p => p.X);
                float minY = 0; // Set minimum to 0 for memory usage
                float maxY = dataPoints.Max(p => p.Y);

                // Add debug logging or breakpoint here to check values
                System.Diagnostics.Debug.WriteLine($"Data points count: {dataPoints.Count}");
                System.Diagnostics.Debug.WriteLine($"X range: {minX} to {maxX}");
                System.Diagnostics.Debug.WriteLine($"Y range: {minY} to {maxY}");

                // Handle single point case and prevent division by zero
                if (Math.Abs(maxX - minX) < float.Epsilon) maxX = minX + 1;
                if (Math.Abs(maxY - minY) < float.Epsilon) maxY = minY + 1;

                // Modified scaling calculation with padding
                const int padding = 20;
                List<Point> scaledPoints = dataPoints.Select(p => new Point(
                    (int)(padding + (p.X - minX) / (maxX - minX) * (Width - 2 * padding)),
                    (int)(Height - padding - ((p.Y - minY) / (maxY - minY) * (Height - 2 * padding)))
                )).ToList();

                // Add debug logging for scaled points
                System.Diagnostics.Debug.WriteLine($"Scaled points count: {scaledPoints.Count}");
                foreach (var p in scaledPoints)
                {
                    System.Diagnostics.Debug.WriteLine($"Scaled point: ({p.X}, {p.Y})");
                }

                // Draw lines between points
                for (int i = 0; i < scaledPoints.Count - 1; i++)
                {
                    g.DrawLine(dataPen, scaledPoints[i], scaledPoints[i + 1]);
                }

                // Draw points
                using (SolidBrush brush = new SolidBrush(Color.Blue))
                {
                    foreach (Point p in scaledPoints)
                    {
                        //g.FillEllipse(brush, p.X - 1, p.Y - 1, 2, 2);
                    }
                }
            }
        }
    }
} 
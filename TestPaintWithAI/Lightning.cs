using System;
using System.Drawing;
using System.Collections.Generic;

public class Lightning
{
    private List<Point> points;
    private int lifetime;
    private static Random random = new Random();
    private int alpha;
    private bool isActive;

    public Lightning(int startX, int startY, int endY)
    {
        points = new List<Point>();
        GeneratePath(startX, startY, endY);
        lifetime = 3; // Will show for 3 frames
        alpha = 255;
        isActive = true;
    }

    private void GeneratePath(int startX, int startY, int endY)
    {
        points.Clear();
        points.Add(new Point(startX, startY));
        
        int currentY = startY;
        while (currentY < endY)
        {
            int lastX = points[points.Count - 1].X;
            currentY += random.Next(20, 50);
            int newX = lastX + random.Next(-50, 51);
            points.Add(new Point(newX, currentY));
        }
    }

    public bool Update()
    {
        if (!isActive) return false;
        
        lifetime--;
        alpha = (lifetime * 255) / 3;
        return lifetime > 0;
    }

    public void Draw(Graphics g)
    {
        if (!isActive || points.Count < 2) return;

        using (Pen lightningPen = new Pen(Color.FromArgb(alpha, 200, 200, 255), 2))
        {
            for (int i = 1; i < points.Count; i++)
            {
                g.DrawLine(lightningPen, points[i - 1], points[i]);
            }
        }
    }
} 
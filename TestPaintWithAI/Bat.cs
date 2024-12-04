using System;
using System.Drawing;

public class Bat
{
    private float x, y;
    private float speedX, speedY;
    private float amplitude;
    private float frequency;
    private float wingAngle;
    private float wingSpeed;
    private static Random random = new Random();

    public Bat(int formWidth, int formHeight)
    {
        x = random.Next(formWidth);
        y = random.Next(formHeight / 2);
        speedX = (float)(random.NextDouble() * 2 + 1);
        speedY = (float)(random.NextDouble() * 0.5 + 0.5);
        amplitude = random.Next(20, 50);
        frequency = (float)(random.NextDouble() * 0.1 + 0.05);
        wingAngle = 0;
        wingSpeed = 0.5f;
    }

    public void Update(int formWidth, int formHeight)
    {
        x += speedX;
        y += (float)(amplitude * Math.Sin(frequency * x));

        wingAngle += wingSpeed;
        if (wingAngle > Math.PI * 2)
        {
            wingAngle -= (float)(Math.PI * 2);
        }

        if (x > formWidth) x = -20;
        if (y < 0) y = formHeight / 2;
    }

    public void Draw(Graphics g)
    {
        using (Pen batPen = new Pen(Color.Red, 1))
        {
            g.DrawEllipse(batPen, x - 3, y - 3, 6, 6);

            float wingY = (float)(Math.Sin(wingAngle) * 8);

            g.DrawLine(batPen, x - 3, y, x - 10, y - 5 + wingY);
            g.DrawLine(batPen, x + 3, y, x + 10, y - 5 - wingY);

            g.DrawLine(batPen, x - 10, y - 5 + wingY, x - 13, y - 3 + wingY * 1.2f);
            g.DrawLine(batPen, x + 10, y - 5 - wingY, x + 13, y - 3 - wingY * 1.2f);
        }
    }
} 
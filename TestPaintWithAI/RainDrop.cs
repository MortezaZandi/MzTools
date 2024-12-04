using System;
using System.Drawing;

public class RainDrop
{
    private float x, y;
    private float speed;
    private float length;
    private static Random random = new Random(); // Static Random instance

    public RainDrop(int formWidth, int formHeight)
    {
        Reset(formWidth, formHeight, true);
        y = random.Next(0, formHeight); // Start at random height initially
    }

    private void Reset(int formWidth, int formHeight, bool randomX = false)
    {
        x = randomX ? random.Next(0, formWidth) : random.Next(0, formWidth);
        y = -10; // Start above the form
        speed = random.Next(15, 25);
        length = random.Next(10, 20);
    }

    public void Update(int formWidth, int formHeight)
    {
        y += speed;
        if (y > formHeight)
        {
            Reset(formWidth, formHeight);
        }
    }

    public void Draw(Graphics g)
    {
        using (Pen pen = new Pen(Color.FromArgb(180, 200, 200, 255), 1))
        {
            g.DrawLine(pen, x, y, x - 1, y + length);
        }
    }
} 
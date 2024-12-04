using System;
using System.Drawing;

public class Bomb
{
    private float x, y;
    private float speed;
    private bool exploded; 
    private int explosionRadius;
    private int explosionMaxRadius;
    private int explosionGrowthRate;
    private static Random random = new Random();

    public Bomb(int startX, int startY)
    {
        x = startX;
        y = startY;
        speed = 5;
        exploded = false;
        explosionRadius = 0;
        explosionMaxRadius = 100;
        explosionGrowthRate = 5;
    }

    public bool Update(int groundY)
    {
        if (!exploded)
        {
            y += speed;
            if (y >= groundY)
            {
                exploded = true;
            }
        }
        else
        {
            explosionRadius += explosionGrowthRate;
            if (explosionRadius >= explosionMaxRadius)
            {
                return false; // Explosion finished
            }
        }
        return true;
    }

    public void Draw(Graphics g)
    {
        if (!exploded)
        {
            using (SolidBrush bombBrush = new SolidBrush(Color.Black))
            {
                g.FillEllipse(bombBrush, x - 5, y - 5, 10, 10);
            }
        }
        else
        {
            using (SolidBrush explosionBrush = new SolidBrush(Color.FromArgb(255, 255, 100, 0)))
            {
                g.FillEllipse(explosionBrush, x - explosionRadius, y - explosionRadius, explosionRadius * 2, explosionRadius * 2);
            }
        }
    }
} 
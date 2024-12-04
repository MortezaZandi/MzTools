using System;
using System.Drawing;

public class Star
{
    private float x, y;
    private float brightness;
    private float twinkleSpeed;
    private float twinklePhase;
    private static Random random = new Random();

    public Star(int x, int y)
    {
        this.x = x;
        this.y = y;
        twinkleSpeed = (float)(random.NextDouble() * 0.1 + 0.05);
        twinklePhase = (float)(random.NextDouble() * Math.PI * 2);
        brightness = (float)(random.NextDouble() * 0.5 + 0.5);
    }

    public void Update()
    {
        twinklePhase += twinkleSpeed;
        if (twinklePhase > Math.PI * 2)
            twinklePhase -= (float)(Math.PI * 2);
    }

    public void Draw(Graphics g)
    {
        // Calculate current brightness with twinkling effect
        float currentBrightness = brightness * (0.7f + 0.3f * (float)Math.Sin(twinklePhase));
        int alpha = (int)(255 * currentBrightness);

        using (SolidBrush starBrush = new SolidBrush(Color.FromArgb(alpha, 255, 250, 250)))
        {
            g.FillEllipse(starBrush, x - 1, y - 1, 2, 2);
        }
    }
} 
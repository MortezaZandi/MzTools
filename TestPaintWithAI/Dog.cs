using System;
using System.Drawing;

public class Dog
{
    private float x;
    private float legAngle;
    private float speed;
    private bool isWalkingRight;
    private StickFigure owner;
    private float leashOffset = 30; // Distance from owner

    public Dog(StickFigure owner, float speed)
    {
        this.owner = owner;
        this.speed = Math.Abs(speed);
        this.legAngle = 0;
        this.isWalkingRight = true;
    }

    public void Update(int formWidth)
    {
        legAngle += 15; // Faster leg movement for dog
        if (legAngle > 360)
            legAngle = 0;
    }

    public void Draw(Graphics g, float centerY)
    {
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        // Get position relative to owner
        x = owner.GetX() + (owner.IsWalkingRight() ? leashOffset : -leashOffset);
        isWalkingRight = owner.IsWalkingRight();
        float scaleX = isWalkingRight ? 1 : -1;
        
        // Adjust drawY to match human's ground level
        float groundY = centerY + 60;  // Same level as human's feet
        float drawY = groundY - 15;    // Lift dog up by its height so feet touch ground

        // Use light gray color for the dog
        Color dogColor = Color.FromArgb(255, 220, 220, 220);  // Light gray to match humans

        using (Pen pen = new Pen(dogColor, 2))
        {
            // Draw body (oval)
            g.DrawEllipse(pen, x - 15, drawY - 15, 30, 20);

            // Draw head
            float headX = x + (10 * scaleX);
            g.DrawEllipse(pen, headX - 8, drawY - 20, 16, 16);

            // Draw ear
            float earX = headX + (2 * scaleX);
            g.DrawEllipse(pen, earX - 4, drawY - 24, 8, 8);

            // Draw eye
            float eyeX = headX + (2 * scaleX);
            using (SolidBrush brush = new SolidBrush(dogColor))
            {
                g.FillEllipse(brush, eyeX, drawY - 18, 3, 3);
            }

            // Draw nose
            float noseX = headX + (6 * scaleX);
            using (SolidBrush brush = new SolidBrush(dogColor))
            {
                g.FillEllipse(brush, noseX, drawY - 14, 4, 4);
            }

            // Draw tail
            float tailStartX = x - (15 * scaleX);
            float tailAngle = (legAngle + 90) * (float)Math.PI / 180;
            float tailEndX = tailStartX - (float)(10 * Math.Cos(tailAngle) * scaleX);
            float tailEndY = drawY - 10 + (float)(5 * Math.Sin(tailAngle));
            g.DrawLine(pen, tailStartX, drawY - 10, tailEndX, tailEndY);

            // Draw legs
            float frontLegX = (float)(8 * Math.Sin(legAngle * Math.PI / 180));
            float backLegX = (float)(8 * Math.Sin((legAngle + 180) * Math.PI / 180));

            // Draw all legs
            g.DrawLine(pen, x + (10 * scaleX), drawY + 5,
                         x + (10 * scaleX) + (frontLegX * scaleX), groundY);
            g.DrawLine(pen, x + (5 * scaleX), drawY + 5,
                         x + (5 * scaleX) + (backLegX * scaleX), groundY);
            g.DrawLine(pen, x - (5 * scaleX), drawY + 5,
                         x - (5 * scaleX) + (frontLegX * scaleX), groundY);
            g.DrawLine(pen, x - (10 * scaleX), drawY + 5,
                         x - (10 * scaleX) + (backLegX * scaleX), groundY);

            // Draw collar
            using (Pen collarPen = new Pen(Color.FromArgb(255, 180, 134, 90), 2))  // Match leash color
            {
                g.DrawLine(collarPen, headX - (4 * scaleX), drawY - 12,
                                    headX + (4 * scaleX), drawY - 12);
            }
        }
    }
} 
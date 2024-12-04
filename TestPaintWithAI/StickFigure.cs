using System;
using System.Drawing;

public class StickFigure
{
    private float x;
    private float legAngle;
    private float speed;
    private bool isWalkingRight;
    private Dog dog;
    private float rightHandX;
    private float handY;

    public float GetX() { return x; }
    public bool IsWalkingRight() { return isWalkingRight; }

    public StickFigure(float startX, float speed)
    {
        this.x = startX;
        this.legAngle = 0;
        this.speed = Math.Abs(speed);
        this.isWalkingRight = true;
        this.dog = new Dog(this, this.speed * 1.2f);
    }

    public void Update(int formWidth)
    {
        if (isWalkingRight)
        {
            x += speed;
            if (x > formWidth - 50)
            {
                isWalkingRight = false;
                x = formWidth - 50;
            }
        }
        else
        {
            x -= speed;
            if (x < 50)
            {
                isWalkingRight = true;
                x = 50;
            }
        }

        legAngle += 10;
        if (legAngle > 360)
            legAngle = 0;

        dog.Update(formWidth);
    }

    public void Draw(Graphics g, float centerY)
    {
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        float drawX = x;
        float scaleX = isWalkingRight ? 1 : -1;

        // Use light gray color for the figure
        Color figureColor = Color.FromArgb(255, 220, 220, 220);  // Light gray

        using (Pen pen = new Pen(figureColor, 2))
        {
            g.DrawEllipse(pen, drawX - 10, centerY - 50, 20, 20);
            
            using (SolidBrush hatBrush = new SolidBrush(Color.FromArgb(255, 180, 134, 90)))  // Lighter brown
            {
                g.FillRectangle(hatBrush, drawX - 12, centerY - 55, 24, 4);
                g.FillRectangle(hatBrush, drawX - 8, centerY - 65, 16, 10);
            }

            float eyeX = drawX + (2 * scaleX);
            float eyeY = centerY - 42;
            using (Pen eyePen = new Pen(figureColor, 1.5f))
            {
                g.DrawEllipse(eyePen, eyeX, eyeY, 4, 4);
                using (SolidBrush brush = new SolidBrush(figureColor))
                {
                    g.FillEllipse(brush, eyeX + (1 * scaleX), eyeY + 1, 2, 2);
                }
            }

            // Rest of the drawing code remains the same but uses the figureColor
            g.DrawLine(pen, drawX, centerY - 30, drawX, centerY + 10);

            float leftShoulderX = drawX - (15 * scaleX);
            float rightShoulderX = drawX + (15 * scaleX);
            float shoulderY = centerY - 20;

            float leftHandX = leftShoulderX + (float)((20 * Math.Sin((legAngle + 180) * Math.PI / 180)) * scaleX);
            rightHandX = rightShoulderX + (float)((20 * Math.Sin(legAngle * Math.PI / 180)) * scaleX);
            handY = shoulderY + 25 + (float)(5 * Math.Abs(Math.Sin(legAngle * Math.PI / 180)));

            g.DrawLine(pen, leftShoulderX, shoulderY, leftHandX, handY);
            g.DrawLine(pen, rightShoulderX, shoulderY, rightHandX, handY);

            float handSize = 3;
            g.DrawEllipse(pen, leftHandX - handSize, handY - handSize, handSize * 2, handSize * 2);
            g.DrawEllipse(pen, rightHandX - handSize, handY - handSize, handSize * 2, handSize * 2);

            g.DrawLine(pen, leftShoulderX, shoulderY, rightShoulderX, shoulderY);

            float shoulderRadius = 1.5f;
            g.DrawEllipse(pen, leftShoulderX - shoulderRadius, shoulderY - shoulderRadius, shoulderRadius * 2, shoulderRadius * 2);
            g.DrawEllipse(pen, rightShoulderX - shoulderRadius, shoulderY - shoulderRadius, shoulderRadius * 2, shoulderRadius * 2);

            // Draw legs and feet
            float leftLegX = (float)(15 * Math.Sin(legAngle * Math.PI / 180)) * scaleX;
            float rightLegX = (float)(15 * Math.Sin((legAngle + 180) * Math.PI / 180)) * scaleX;

            float leftFootAngle = (legAngle + 180) * (float)Math.PI / 180;
            float rightFootAngle = legAngle * (float)Math.PI / 180;

            // Draw all the leg and foot lines with the same pen (figureColor)
            float leftFootStartX = drawX + (leftLegX * 1.5f);
            float leftFootStartY = centerY + 60;
            g.DrawLine(pen, drawX, centerY + 10, drawX + leftLegX, centerY + 35);
            g.DrawLine(pen, drawX + leftLegX, centerY + 35, leftFootStartX, leftFootStartY);
            
            float leftFootEndX = leftFootStartX + (float)((10 * Math.Cos(leftFootAngle)) * scaleX);
            float leftFootEndY = leftFootStartY + (float)(5 * Math.Sin(leftFootAngle));
            g.DrawLine(pen, leftFootStartX, leftFootStartY, leftFootEndX, leftFootEndY);
            g.DrawLine(pen, leftFootEndX, leftFootEndY, leftFootEndX + (12 * scaleX), leftFootEndY);

            float rightFootStartX = drawX + (rightLegX * 1.5f);
            float rightFootStartY = centerY + 60;
            g.DrawLine(pen, drawX, centerY + 10, drawX + rightLegX, centerY + 35);
            g.DrawLine(pen, drawX + rightLegX, centerY + 35, rightFootStartX, rightFootStartY);
            
            float rightFootEndX = rightFootStartX + (float)((10 * Math.Cos(rightFootAngle)) * scaleX);
            float rightFootEndY = rightFootStartY + (float)(5 * Math.Sin(rightFootAngle));
            g.DrawLine(pen, rightFootStartX, rightFootStartY, rightFootEndX, rightFootEndY);
            g.DrawLine(pen, rightFootEndX, rightFootEndY, rightFootEndX + (12 * scaleX), rightFootEndY);
        }

        // Draw leash with lighter brown color
        using (Pen leashPen = new Pen(Color.FromArgb(255, 180, 134, 90), 1))
        {
            leashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            
            float dogX = x + (isWalkingRight ? 30 : -30);
            float dogHeadX = dogX + (10 * scaleX);
            float collarY = centerY + 45;
            
            g.DrawBezier(leashPen,
                rightHandX, handY,
                rightHandX + (isWalkingRight ? 10 : -10), handY - 10,
                dogHeadX - (isWalkingRight ? 10 : -10), collarY - 10,
                dogHeadX, collarY);
        }

        // Draw the dog
        dog.Draw(g, centerY);
    }
} 
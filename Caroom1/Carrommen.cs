﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caroom1
{
    public abstract class Carrommen : GameObject
    {
        public Carrommen()
        {
            Size = new Size(60, 60);
        }

        protected override void InternalDraw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillEllipse(new SolidBrush(FillColor), Location.X, Location.Y, Size.Width, Size.Height);
            g.DrawEllipse(new Pen(BorderColor, BorderSize), Location.X, Location.Y, Size.Width, Size.Height);

            var p = new Pen(Color.Yellow, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };

            if (!string.IsNullOrEmpty(Name))
            {
                g.DrawString(Name, new Font("Tahoma", 9), Brushes.White, CenterPoint, new StringFormat { Alignment = StringAlignment.Center });
            }

            g.DrawEllipse(p, Circle.Rect);
        }

        //public void MoveTowardsTarget()
        //{
        //    // Calculate direction vector
        //    float directionX = Target.X - Location.X;
        //    float directionY = Target.Y - Location.Y;

        //    // Calculate distance to target
        //    float distance = (float)Math.Sqrt(directionX * directionX + directionY * directionY);

        //    // Normalize direction and scale by speed
        //    if (distance > 0)
        //    {
        //        float stepX = (directionX / distance) * Speed;
        //        float stepY = (directionY / distance) * Speed;

        //        // Update location
        //        Location = new PointF(Location.X + stepX, Location.Y + stepY);



        //        //double check
        //        directionX = Target.X - Location.X;
        //        directionY = Target.Y - Location.Y;
        //        distance = (float)Math.Sqrt(directionX * directionX + directionY * directionY);

        //        if (distance <= 0)
        //        {
        //            Speed = 0;
        //        }
        //    }
        //    else
        //    {
        //        Speed = 0;
        //    }
        //}
    }

    public class BlackCarrommen : Carrommen
    {
        public BlackCarrommen()
        {
            Location = new Point(800, 400);
            FillColor = Color.Gray;
            BorderColor = Color.FromArgb(140, 140, 140);
        }
    }

    public class WhiteCarrommen : Carrommen
    {
        public WhiteCarrommen()
        {
            Location = new Point(400, 200);
            FillColor = Color.Gray;
            BorderColor = Color.FromArgb(150, 150, 150);
        }
    }

    public class Stricker : Carrommen
    {
        public Stricker()
        {
            Location = new Point(700, 600);
            Size = new Size(100, 100);
            FillColor = Color.Gray;
            BorderColor = Color.FromArgb(140, 140, 140);
            BorderSize = 5;
        }
    }
}

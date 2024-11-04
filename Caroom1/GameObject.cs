using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Caroom1
{
    public abstract class GameObject
    {
        public string Name { get; set; }
        public PointF Location { get; set; }
        public Size Size { get; set; }
        public bool Visible { get; set; }
        public Color FillColor { get; set; }
        public Color BorderColor { get; set; }
        public float BorderSize { get; set; } = 3;
        public float Speed { get; set; } = 6f;
        public PointF Force { get; set; }
        public PointF Target { get { return target; } }
        public bool HasChanged { get; private set; } = true;

        public PointF CenterPoint
        {
            get
            {
                return new PointF(Location.X + Size.Width / 2f, Location.Y + Size.Height / 2f);
            }

        }

        public bool HasTarget
        {
            get
            {
                return target != Point.Empty;
            }
        }

        public bool ContainsPoint(PointF location)
        {
            return new RectangleF(this.Location, this.Size).Contains(location);
        }

        public bool IsColideWith(GameObject other, float offsetX, float offsetY)
        {
            return new RectangleF(other.Location, other.Size).Contains(Location);
        }

        public bool IsColidWith(GameObject other)
        {
            return IsColideWith(other, 0, 0);
        }

        public bool IsCollideWith(PointF p)
        {
            return new RectangleF(Location, Size).Contains(p);
        }

        public Circle Circle
        {
            get
            {
                return new Circle
                {
                    CenterX = CenterPoint.X,
                    CenterY = CenterPoint.Y,
                    Radius = Math.Min(this.Size.Width, this.Size.Height) / 2,
                };
            }
        }

        public bool IsPointInCircle(float x, float y)
        {
            var radius = (Math.Min(Size.Width, Size.Height) / 2) - BorderSize;

            var rect = new RectangleF(CenterPoint.X - radius, CenterPoint.Y - radius, radius * 2, radius * 2);

            if (rect.Contains(x, y))
            {
                double dx = CenterPoint.X - x;
                double dy = CenterPoint.Y - y;
                dx *= dx;
                dy *= dy;
                double distanceSquared = dx + dy;
                double radiusSquared = radius * radius;
                return distanceSquared <= radiusSquared;
            }

            return false;
        }

        public void Update()
        {
            ContinueMoveTowardsTarget();
        }

        public void Draw(Graphics g)
        {
            InternalDraw(g);
            HasChanged = false;
        }

        protected abstract void InternalDraw(Graphics g);

        public void MoveTowardsTarget(PointF targetLocation)
        {
            target = targetLocation;
            targetReached = false;

            //Calculate direction vector
            float directionX = target.X - CenterPoint.X;
            float directionY = target.Y - CenterPoint.Y;

            // Calculate distance to target
            float distance = (float)Math.Sqrt(directionX * directionX + directionY * directionY);

            // Normalize direction and scale by speed
            if (distance > 0)
            {
                float stepX = (directionX / distance) * Speed;
                float stepY = (directionY / distance) * Speed;
                Force = new PointF(stepX, stepY);
            }
        }



        #region Privates

        private bool targetReached;
        private PointF target;


        private void ContinueMoveTowardsTarget()
        {
            if (!targetReached && Target != Point.Empty && Force != Point.Empty)
            {
                var newLocation = new PointF(Location.X + (Speed * Force.X), Location.Y + (Speed * Force.Y));
                var newLocationIsAvailable = CheckIfNewLocationAvailable?.Invoke(this, newLocation) ?? true;

                if (!IsCollideWith(Target) && newLocationIsAvailable)
                {
                    Location = newLocation;
                    HasChanged = true;
                    Force = new PointF(Force.X * 0.95f, Force.Y * 0.95f);
                }
                else
                {
                    targetReached = true;
                }
            }

        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                return Name;
            }

            return base.ToString();
        }

        public delegate bool LocationAvailabilityCheckEventHandler(GameObject gameObject, PointF location);
        public event LocationAvailabilityCheckEventHandler CheckIfNewLocationAvailable;

        #endregion
    }
}

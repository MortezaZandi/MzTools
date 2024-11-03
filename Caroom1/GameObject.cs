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
        public float Speed { get; set; } = 10f;
        public PointF Force { get; set; }
        public GameObject Target { get { return target; } }
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
                return target != null;
            }
        }

        public bool ContainsPoint(PointF location)
        {
            return new RectangleF(this.Location, this.Size).Contains(location);
        }

        public bool IsColide(GameObject other, float offsetX, float offsetY)
        {
            return new RectangleF(other.Location, other.Size).Contains(Location);
        }

        public bool IsColided(GameObject other)
        {
            return IsColide(other, 0, 0);
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

        public void SetTarget(GameObject targetObject)
        {
            target = targetObject;
            targetReached = false;
        }

        public void MoveTowardsTarget()
        {
            //Calculate direction vector
            float directionX = target.CenterPoint.X - Location.X;
            float directionY = target.CenterPoint.Y - Location.Y;

            // Calculate distance to target
            float distance = (float)Math.Sqrt(directionX * directionX + directionY * directionY);

            // Normalize direction and scale by speed
            if (distance > 0)
            {
                float stepX = (directionX / distance) * Speed;
                float stepY = (directionY / distance) * Speed;
                Force = new PointF(stepX, stepY);
            }
            else
            {
                Force = PointF.Empty;
            }
        }



        #region Privates

        private bool targetReached;
        private GameObject target;


        private void ContinueMoveTowardsTarget()
        {
            if (!targetReached && Target != null && Force != Point.Empty)
            {
                if (!IsColide(Target, Speed * Force.X, Speed * Force.Y))
                {
                    Location = new PointF(Location.X + (Speed * Force.X), Location.Y + (Speed * Force.Y));
                    HasChanged = true;
                }
                else
                {
                    targetReached = true;
                }
            }

        }
        #endregion
    }
}

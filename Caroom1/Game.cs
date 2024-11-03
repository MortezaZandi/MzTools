using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caroom1
{
    internal class Game
    {
        public List<Carrommen> carrommens;
        private ShootEffect ShootEffect { get; set; }

        public Game()
        {
            this.carrommens = new List<Carrommen>();
            this.ShootEffect = new ShootEffect();
        }

        public void StartNewGame()
        {
            carrommens.Clear();
            carrommens.Add(new BlackCarrommen());
            carrommens.Add(new WhiteCarrommen());
            carrommens.Add(new Stricker());

            ArrangeCarrommens();
        }

        private void ArrangeCarrommens()
        {
            carrommens[0].Location = new System.Drawing.Point(200, 200);
            carrommens[1].Location = new System.Drawing.Point(700, 100);
            carrommens[2].Location = new System.Drawing.Point(600, 400);
        }

        public void Draw(Graphics g)
        {
            g.Clear(Color.Tan);

            foreach (var c in carrommens)
            {
                c.Draw(g);
            }

            if (this.ShootEffect.IsValid)
            {
                g.DrawLine(new Pen(Color.Black, 3), this.ShootEffect.Start, ShootEffect.End);

                if (GameStriker.HasTarget)
                {
                    g.FillEllipse(Brushes.Red, GameStriker.CenterPoint.Inflate(3));
                }
            }
        }

        public Stricker GameStriker
        {
            get
            {
                return carrommens[2] as Stricker;
            }
        }

        //public bool NeedUpdate
        //{
        //    get
        //    {
        //        return GameStriker.Speed > 0;
        //    }
        //}

        public void ClearShootEffect()
        {
            this.ShootEffect.IsValid = false;
        }

        public void SetShootEffect(PointF start, PointF end)
        {
            this.ShootEffect.Start = start;
            this.ShootEffect.End = end;
            this.ShootEffect.IsValid = true;

            SetTarget(start, end);
        }


        private void SetTarget(PointF start, PointF end)
        {
            //if (!GameStriker.HasTarget)
            {
                var targetPoint = new PointF(
                 start.X + (start.X - end.X),
                 start.Y + (start.Y - end.Y));

                var targetObject = GetGameObjectByPoint(targetPoint);

                if (targetObject != null && targetObject != GameStriker)
                {
                    GameStriker.SetTarget(targetObject);
                }
            }
        }

        public GameObject GetGameObjectByPoint(PointF p)
        {
            foreach (var item in carrommens)
            {
                if (item.ContainsPoint(p))
                {
                    return item;
                }
            }

            return null;
        }

        internal void HitStriker()
        {
            GameStriker.MoveTowardsTarget();
        }

        public bool Update()
        {
            var anyObjectChanged = false;

            foreach (var item in carrommens)
            {
                item.Update();

                anyObjectChanged |= item.HasChanged;
            }

            return anyObjectChanged;
        }
    }


    public class ShootEffect
    {
        public PointF Start { get; set; }
        public PointF End { get; set; }
        public bool IsValid { get; set; }
    }
}

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

        public Game()
        {
            this.carrommens = new List<Carrommen>();
        }

        public void StartNewGame()
        {
            carrommens.Clear();
            carrommens.Add(new BlackCarrommen() { Name = "BlackC1" });
            carrommens.Add(new WhiteCarrommen() { Name = "WhiteC2" });
            carrommens.Add(new Stricker() { Name = "Striker" });

            foreach (var item in carrommens)
            {
                item.CheckIfNewLocationAvailable += Item_CheckIfNewLocationAvailable;
            }

            ArrangeCarrommens();
        }

        private bool Item_CheckIfNewLocationAvailable(GameObject gameObject, PointF location)
        {
            var c = new Circle()
            {
                CenterX = location.X + gameObject.Size.Width / 2,
                CenterY = location.Y + gameObject.Size.Height / 2,
                Radius = gameObject.Circle.Radius
            };

            foreach (var other in carrommens)
            {
                if (other != gameObject)
                {
                    if (c.IsCollideWith(other.Circle))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void ArrangeCarrommens()
        {
            carrommens[0].Location = new System.Drawing.Point(200, 200);
            carrommens[1].Location = new System.Drawing.Point(700, 100);
            carrommens[2].Location = new System.Drawing.Point(600, 400);
        }

        public void Draw(Graphics g)
        {

            foreach (var c in carrommens)
            {
                c.Draw(g);
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
        public PointF Target { get; set; }

    }
}

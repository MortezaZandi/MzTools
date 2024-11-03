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

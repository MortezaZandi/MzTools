using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caroom1
{
    public partial class Form1 : Form
    {
        private Game game;
        private PointF _mosueDownLocation;
        private PointF _mosueUpLocation;
        private ShootEffect ShootEffect { get; set; }

        public Form1()
        {
            InitializeComponent();
            game = new Game();
            game.StartNewGame();
            this.ShootEffect = new ShootEffect();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.Clear(SystemColors.GradientInactiveCaption);

            game.Draw(e.Graphics);

            if (this.ShootEffect.IsValid)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 3), this.ShootEffect.Start, ShootEffect.End);

                e.Graphics.DrawLine(Pens.Gray, game.GameStriker.CenterPoint, ShootEffect.Target);

            }
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (game.GameStriker.ContainsPoint(e.Location))
            {
                _mosueDownLocation = game.GameStriker.CenterPoint;
                SetShootEffect(game.GameStriker.CenterPoint, _mosueDownLocation);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (_mosueDownLocation != Point.Empty)
            {
                _mosueUpLocation = e.Location;
                game.GameStriker.MoveTowardsTarget(ShootEffect.Target);
                ClearShootEffect();

                _mosueDownLocation = Point.Empty;
                _mosueUpLocation = Point.Empty;
                Invalidate();
            }
        }



        public PointF GetTarget(PointF start, PointF end)
        {
            return new PointF(
             start.X + (start.X - end.X),
             start.Y + (start.Y - end.Y));
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mosueDownLocation != Point.Empty)
            {
                SetShootEffect(_mosueDownLocation, e.Location);
                this.Invalidate();
            }

            if (game.GameStriker.IsPointInCircle(e.X, e.Y))
            {
                game.GameStriker.FillColor = Color.Green;
            }
            else
            {
                game.GameStriker.FillColor = Color.Maroon;
            }

            Invalidate();
        }

        private void tmrUpdate100Ms_Tick(object sender, EventArgs e)
        {
            if (game.Update())
            {
                Invalidate();
            }
        }


        public void ClearShootEffect()
        {
            this.ShootEffect.IsValid = false;
        }

        public void SetShootEffect(PointF start, PointF end)
        {
            this.ShootEffect.Start = start;
            this.ShootEffect.End = end;
            this.ShootEffect.IsValid = true;
            this.ShootEffect.Target = GetTarget(start, end);
        }
    }


}



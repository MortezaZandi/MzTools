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

        public Form1()
        {
            InitializeComponent();
            game = new Game();
            game.StartNewGame();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            game.Draw(e.Graphics);
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (game.GameStriker.ContainsPoint(e.Location))
            {
                _mosueDownLocation = game.GameStriker.CenterPoint;
                game.SetShootEffect(game.GameStriker.CenterPoint, _mosueDownLocation);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (_mosueDownLocation != Point.Empty)
            {
                game.ClearShootEffect();
                _mosueUpLocation = e.Location;
                Shoot();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mosueDownLocation != Point.Empty)
            {
                game.SetShootEffect(_mosueDownLocation, e.Location);
                this.Invalidate();
            }
        }

        private void Shoot()
        {
            game.HitStriker();
            _mosueDownLocation = Point.Empty;
            _mosueUpLocation = Point.Empty;
            Invalidate();
        }

        private void tmrUpdate100Ms_Tick(object sender, EventArgs e)
        {
            if (game.Update())
            {
                Invalidate();
            }
        }
    }


}



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPaintWithAI
{
    public partial class Form1 : Form
    {
        private List<StickFigure> figures;
        private List<RainDrop> raindrops;
        private List<Lightning> lightnings;
        private List<Building> buildings;
        private List<Star> stars;
        private Timer animationTimer;
        private const int RAINDROP_COUNT = 100;
        private const int STAR_COUNT = 200;
        private static Random random = new Random();
        private int lightningCounter;
        private Point moonPosition;
        private int moonRadius = 50;
        private Bat bat;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            figures = new List<StickFigure>();
            raindrops = new List<RainDrop>();
            lightnings = new List<Lightning>();
            buildings = new List<Building>();
            stars = new List<Star>();

            // Initialize more stars and spread them across the entire sky
            for (int i = 0; i < STAR_COUNT; i++)
            {
                int starX = random.Next(0, this.ClientSize.Width);
                // Place stars from top to ground level
                int starY = random.Next(0, this.ClientSize.Height - 100);  // Leave some space at bottom
                stars.Add(new Star(starX, starY));
            }

            // Initialize buildings
            int buildingCount = this.ClientSize.Width / 100;
            for (int i = 0; i < buildingCount; i++)
            {
                int buildingWidth = random.Next(60, 100);
                int buildingHeight = random.Next(100, 300);
                int buildingX = i * 100;
                buildings.Add(new Building(buildingX, buildingWidth, buildingHeight));
            }

            figures.Add(new StickFigure(50, 3));
            figures.Add(new StickFigure(200, 4));

            for (int i = 0; i < RAINDROP_COUNT; i++)
            {
                raindrops.Add(new RainDrop(this.ClientSize.Width, this.ClientSize.Height));
            }

            lightningCounter = random.Next(20, 100);

            // Initialize moon position
            moonPosition = new Point(this.ClientSize.Width - 100, 100);

            // Initialize bat
            bat = new Bat(this.ClientSize.Width, this.ClientSize.Height);

            animationTimer = new Timer();
            animationTimer.Interval = 50;
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            foreach (var star in stars)
            {
                star.Update();
            }

            foreach (var building in buildings)
            {
                building.Update();
            }

            foreach (var figure in figures)
            {
                figure.Update(this.ClientSize.Width);
            }

            foreach (var drop in raindrops)
            {
                drop.Update(this.ClientSize.Width, this.ClientSize.Height);
            }

            lightnings.RemoveAll(l => !l.Update());

            lightningCounter--;
            if (lightningCounter <= 0)
            {
                int startX = random.Next(0, this.ClientSize.Width);
                lightnings.Add(new Lightning(startX, 0, this.ClientSize.Height));
                lightningCounter = random.Next(20, 100);
            }

            // Update bat
            bat.Update(this.ClientSize.Width, this.ClientSize.Height);

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw darker background for night sky
            using (SolidBrush backgroundBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 20)))
            {
                e.Graphics.FillRectangle(backgroundBrush, this.ClientRectangle);
            }

            // Draw moon
            using (SolidBrush moonBrush = new SolidBrush(Color.FromArgb(255, 230, 230, 230)))
            {
                e.Graphics.FillEllipse(moonBrush, moonPosition.X - moonRadius, moonPosition.Y - moonRadius, moonRadius * 2, moonRadius * 2);
            }

            // Draw stars
            foreach (var star in stars)
            {
                star.Draw(e.Graphics);
            }

            // Calculate new ground position at bottom of form
            float groundY = this.ClientSize.Height - 20;  // 20 pixels from bottom
            float centerY = groundY - 60;  // Adjust figures to stand on new ground position

            // Draw buildings
            foreach (var building in buildings)
            {
                building.Draw(e.Graphics, groundY);
            }

            // Draw lightning
            foreach (var lightning in lightnings)
            {
                lightning.Draw(e.Graphics);
            }

            // Draw rain
            foreach (var drop in raindrops)
            {
                drop.Draw(e.Graphics);
            }

            // Draw ground
            using (Pen groundPen = new Pen(Color.FromArgb(255, 100, 100, 100), 2))
            {
                e.Graphics.DrawLine(groundPen, 0, groundY, this.ClientSize.Width, groundY);
            }

            // Draw figures
            foreach (var figure in figures)
            {
                figure.Draw(e.Graphics, centerY);
            }

            // Draw bat
            bat.Draw(e.Graphics);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            
            // Reinitialize raindrops
            if (raindrops != null)
            {
                raindrops.Clear();
                for (int i = 0; i < RAINDROP_COUNT; i++)
                {
                    raindrops.Add(new RainDrop(this.ClientSize.Width, this.ClientSize.Height));
                }
            }

            // Reinitialize buildings to fill the new width
            if (buildings != null)
            {
                buildings.Clear();
                int buildingCount = this.ClientSize.Width / 100;  // One building every 100 pixels
                for (int i = 0; i < buildingCount; i++)
                {
                    int buildingWidth = random.Next(60, 100);
                    int buildingHeight = random.Next(100, 300);
                    int buildingX = i * 100;
                    buildings.Add(new Building(buildingX, buildingWidth, buildingHeight));
                }
            }

            // Reinitialize stars to cover the new form size
            if (stars != null)
            {
                stars.Clear();
                for (int i = 0; i < STAR_COUNT; i++)
                {
                    int starX = random.Next(0, this.ClientSize.Width);
                    int starY = random.Next(0, this.ClientSize.Height - 100);
                    stars.Add(new Star(starX, starY));
                }
            }

            // Update moon position on resize
            moonPosition = new Point(this.ClientSize.Width - 100, 100);
        }
    }
}

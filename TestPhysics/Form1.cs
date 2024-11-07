using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPhysics
{
    public partial class Form1 : Form
    {
        private List<Shape> shapes;
        private ShapeDragger dragger;
        private readonly CollisionDetector collisionDetector;

        public Form1()
        {
            InitializeComponent();

            shapes = new List<Shape>();
            CreateShapes();
            collisionDetector = new CollisionDetector(shapes);
            dragger = new ShapeDragger(this, shapes, collisionDetector);
        }

        private void CreateShapes()
        {
            shapes.Add(new Circle
            {
                X = 100,
                Y = 200,
                Width = 100,
                Height = 100,
                BackColor = Color.LightGray,
                BorderColor = Color.Tan,
                Radius = 100,
            });

            shapes.Add(new Circle
            {
                X = 300,
                Y = 300,
                Width = 100,
                Height = 100,
                BackColor = Color.Bisque,
                Radius = 100,
            });
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            foreach (var shape in shapes)
            {
                shape.Draw(e.Graphics);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestCarromGameWithAI
{
    public partial class Form1 : Form
    {
        // Game elements
        private Point strikerStartPosition;
        private Point strikerCurrentPosition;
        private bool isDragging = false;
        private float power = 0;
        private const float MAX_POWER = 100;
        
        // Player turn
        private bool isPlayer1Turn = true; // true = white, false = black
        
        // Carroman pieces
        private List<CarromPiece> carromPieces = new List<CarromPiece>();
        private CarromPiece striker;

        private Timer gameTimer;
        private const float FRICTION = 0.98f;
        private const int POCKET_RADIUS = 30;
        private List<Point> pockets;

        public Form1()
        {
            InitializeComponent();
            
            // Enable double buffering to prevent flickering
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            
            InitializeGame();
            
            // Add mouse events
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            this.Paint += Form1_Paint;

            // Initialize game timer
            gameTimer = new Timer();
            gameTimer.Interval = 16; // ~60 FPS
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }

        private void InitializeGame()
        {
            // Initialize striker with a more visible color and size
            striker = new CarromPiece(Color.Red, new Point(this.ClientSize.Width / 2, this.ClientSize.Height - 100), 20, true);
            ResetStrikerPosition();

            // Initialize carrom pieces (9 white and 9 black)
            InitializeCarromPieces();

            // Initialize pockets
            pockets = new List<Point>
            {
                new Point(50, 50),
                new Point(this.ClientSize.Width - 50, 50),
                new Point(50, this.ClientSize.Height - 50),
                new Point(this.ClientSize.Width - 50, this.ClientSize.Height - 50)
            };
        }

        private void InitializeCarromPieces()
        {
            // Center of the board
            Point center = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
            int pieceRadius = 15;
            
            // Create formation of pieces
            for (int i = 0; i < 9; i++)
            {
                // White pieces
                carromPieces.Add(new CarromPiece(Color.White, 
                    new Point(center.X + (i % 3) * 30 - 30, center.Y + (i / 3) * 30 - 30), 
                    pieceRadius, false));
                
                // Black pieces
                carromPieces.Add(new CarromPiece(Color.Black, 
                    new Point(center.X + (i % 3) * 30, center.Y + (i / 3) * 30), 
                    pieceRadius, false));
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsPointNearStriker(e.Location))
            {
                isDragging = true;
                strikerCurrentPosition = e.Location;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                strikerCurrentPosition = e.Location;
                power = CalculatePower(strikerStartPosition, strikerCurrentPosition);
                this.Invalidate(); // Redraw the form
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate velocity based on power and direction
                float dx = strikerStartPosition.X - strikerCurrentPosition.X;
                float dy = strikerStartPosition.Y - strikerCurrentPosition.Y;
                float distance = (float)Math.Sqrt(dx * dx + dy * dy);
                
                if (distance > 0)
                {
                    // Limit the maximum power
                    float maxVelocity = 30f;  // Adjust this value to control maximum speed
                    float velocityScale = Math.Min(power / MAX_POWER, 1.0f) * maxVelocity;
                    
                    striker.VelocityX = (dx / distance) * velocityScale;
                    striker.VelocityY = (dy / distance) * velocityScale;
                }

                isDragging = false;
                this.Invalidate();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Draw outer board border
            e.Graphics.FillRectangle(Brushes.SaddleBrown, 40, 40, this.ClientSize.Width - 80, this.ClientSize.Height - 80);
            
            // Draw inner board
            e.Graphics.FillRectangle(Brushes.BurlyWood, 70, 70, this.ClientSize.Width - 140, this.ClientSize.Height - 140);
            
            // Draw pockets
            foreach (var pocket in pockets)
            {
                e.Graphics.FillEllipse(Brushes.Black,
                    pocket.X - POCKET_RADIUS,
                    pocket.Y - POCKET_RADIUS,
                    POCKET_RADIUS * 2,
                    POCKET_RADIUS * 2);
            }
            
            // Draw pieces
            foreach (var piece in carromPieces)
            {
                e.Graphics.FillEllipse(new SolidBrush(piece.Color),
                    piece.Position.X - piece.Radius,
                    piece.Position.Y - piece.Radius,
                    piece.Radius * 2,
                    piece.Radius * 2);
            }
            
            // Draw striker with a border to make it more visible
            e.Graphics.FillEllipse(new SolidBrush(striker.Color),
                striker.Position.X - striker.Radius,
                striker.Position.Y - striker.Radius,
                striker.Radius * 2,
                striker.Radius * 2);
            e.Graphics.DrawEllipse(new Pen(Color.White, 2),
                striker.Position.X - striker.Radius,
                striker.Position.Y - striker.Radius,
                striker.Radius * 2,
                striker.Radius * 2);
            
            // Draw power line when dragging
            if (isDragging)
            {
                e.Graphics.DrawLine(new Pen(Color.White, 2),
                    strikerStartPosition,
                    strikerCurrentPosition);
            }

            // Draw turn indicator
            string turnText = isPlayer1Turn ? "White's Turn" : "Black's Turn";
            e.Graphics.DrawString(turnText, SystemFonts.DefaultFont, Brushes.White, 10, 10);
        }

        private bool IsPointNearStriker(Point point)
        {
            int dx = point.X - striker.Position.X;
            int dy = point.Y - striker.Position.Y;
            return Math.Sqrt(dx * dx + dy * dy) <= striker.Radius;
        }

        private float CalculatePower(Point start, Point end)
        {
            float dx = end.X - start.X;
            float dy = end.Y - start.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);
            return Math.Min(distance / 2, MAX_POWER);
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            UpdateGamePhysics();
            CheckCollisions();
            CheckPockets();
            this.Invalidate();
        }

        private void UpdateGamePhysics()
        {
            // Board boundaries
            int leftBoundary = 70;  // Adjust these values based on your board size
            int rightBoundary = this.ClientSize.Width - 70;
            int topBoundary = 70;
            int bottomBoundary = this.ClientSize.Height - 70;

            // Update carrom pieces
            foreach (var piece in carromPieces)
            {
                // Update position
                float newX = piece.Position.X + piece.VelocityX;
                float newY = piece.Position.Y + piece.VelocityY;

                // Boundary collision for pieces
                if (newX - piece.Radius < leftBoundary || newX + piece.Radius > rightBoundary)
                {
                    piece.VelocityX *= -0.8f;  // Bounce with some energy loss
                    newX = piece.Position.X;  // Prevent sticking to walls
                }
                if (newY - piece.Radius < topBoundary || newY + piece.Radius > bottomBoundary)
                {
                    piece.VelocityY *= -0.8f;  // Bounce with some energy loss
                    newY = piece.Position.Y;  // Prevent sticking to walls
                }

                piece.Position = new Point((int)newX, (int)newY);
                piece.VelocityX *= FRICTION;
                piece.VelocityY *= FRICTION;
            }

            // Update striker
            float strikerNewX = striker.Position.X + striker.VelocityX;
            float strikerNewY = striker.Position.Y + striker.VelocityY;

            // Boundary collision for striker
            if (strikerNewX - striker.Radius < leftBoundary || strikerNewX + striker.Radius > rightBoundary)
            {
                striker.VelocityX *= -0.8f;
                strikerNewX = striker.Position.X;
            }
            if (strikerNewY - striker.Radius < topBoundary || strikerNewY + striker.Radius > bottomBoundary)
            {
                striker.VelocityY *= -0.8f;
                strikerNewY = striker.Position.Y;
            }

            striker.Position = new Point((int)strikerNewX, (int)strikerNewY);
            striker.VelocityX *= FRICTION;
            striker.VelocityY *= FRICTION;
        }

        private void CheckCollisions()
        {
            // Check collisions between striker and carrom pieces
            foreach (var piece in carromPieces)
            {
                if (IsColliding(striker, piece))
                {
                    ResolveCollision(striker, piece);
                }
            }

            // Check collisions between carrom pieces
            for (int i = 0; i < carromPieces.Count; i++)
            {
                for (int j = i + 1; j < carromPieces.Count; j++)
                {
                    if (IsColliding(carromPieces[i], carromPieces[j]))
                    {
                        ResolveCollision(carromPieces[i], carromPieces[j]);
                    }
                }
            }
        }

        private bool IsColliding(CarromPiece a, CarromPiece b)
        {
            float dx = a.Position.X - b.Position.X;
            float dy = a.Position.Y - b.Position.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);
            return distance < a.Radius + b.Radius;
        }

        private void ResolveCollision(CarromPiece a, CarromPiece b)
        {
            // Simple elastic collision resolution
            float dx = b.Position.X - a.Position.X;
            float dy = b.Position.Y - a.Position.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            if (distance == 0) return;

            float nx = dx / distance;
            float ny = dy / distance;

            float p = 2 * (a.VelocityX * nx + a.VelocityY * ny - b.VelocityX * nx - b.VelocityY * ny) / (a.Radius + b.Radius);

            a.VelocityX -= p * a.Radius * nx;
            a.VelocityY -= p * a.Radius * ny;
            b.VelocityX += p * b.Radius * nx;
            b.VelocityY += p * b.Radius * ny;
        }

        private void CheckPockets()
        {
            for (int i = carromPieces.Count - 1; i >= 0; i--)
            {
                var piece = carromPieces[i];
                foreach (var pocket in pockets)
                {
                    if (IsInPocket(piece, pocket))
                    {
                        carromPieces.RemoveAt(i);
                        // Update score based on piece color
                        if (piece.Color == Color.White && isPlayer1Turn)
                        {
                            // Player 1 scores
                        }
                        else if (piece.Color == Color.Black && !isPlayer1Turn)
                        {
                            // Player 2 scores
                        }
                        break;
                    }
                }
            }

            // Check if striker is in pocket
            if (IsInPocket(striker, pockets[0]) || 
                IsInPocket(striker, pockets[1]) || 
                IsInPocket(striker, pockets[2]) || 
                IsInPocket(striker, pockets[3]))
            {
                // Foul - striker went into pocket
                isPlayer1Turn = !isPlayer1Turn;
                ResetStrikerPosition();
            }

            // Check if all pieces have stopped moving
            bool allStopped = carromPieces.All(p => Math.Abs(p.VelocityX) < 0.1 && Math.Abs(p.VelocityY) < 0.1) &&
                              Math.Abs(striker.VelocityX) < 0.1 && Math.Abs(striker.VelocityY) < 0.1;

            if (allStopped)
            {
                isPlayer1Turn = !isPlayer1Turn;
                ResetStrikerPosition();
            }
        }

        private bool IsInPocket(CarromPiece piece, Point pocket)
        {
            float dx = piece.Position.X - pocket.X;
            float dy = piece.Position.Y - pocket.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);
            return distance < POCKET_RADIUS;
        }

        private void ResetStrikerPosition()
        {
            // Position the striker at the bottom for Player 1 (White)
            // or at the top for Player 2 (Black)
            if (isPlayer1Turn)
            {
                striker.Position = new Point(this.ClientSize.Width / 2, this.ClientSize.Height - 100);
            }
            else
            {
                striker.Position = new Point(this.ClientSize.Width / 2, 100);
            }
            
            strikerStartPosition = striker.Position;
            striker.VelocityX = 0;
            striker.VelocityY = 0;
        }
    }

    // Carrom piece class
    public class CarromPiece
    {
        public Color Color { get; set; }
        public Point Position { get; set; }
        public int Radius { get; set; }
        public bool IsStriker { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }

        public CarromPiece(Color color, Point position, int radius, bool isStriker)
        {
            Color = color;
            Position = position;
            Radius = radius;
            IsStriker = isStriker;
            VelocityX = 0;
            VelocityY = 0;
        }
    }
}

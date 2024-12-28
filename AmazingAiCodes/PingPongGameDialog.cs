using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

public class PingPongGameDialog : Form
{
    // Update ball-related fields to handle multiple balls
    private class Ball
    {
        public Rectangle Rect;
        public float SpeedX;
        public float SpeedY;

        public Ball(Rectangle rect, float speedX, float speedY)
        {
            Rect = rect;
            SpeedX = speedX;
            SpeedY = speedY;
        }
    }

    private List<Ball> balls;
    private const int BALL_COUNT = 2;
    
    // Game objects
    private Rectangle player;
    private Rectangle computer;
    
    // Ball movement
    private float ballSpeedX = 5;
    private float ballSpeedY = 5;
    private const float SPEED_INCREASE = 1.1f;
    private const float MAX_SPEED = 15;
    private const float BASE_SPEED = 5;
    
    // Scoring
    private int playerScore = 0;
    private int computerScore = 0;
    
    // Game timer
    private Timer gameTimer;
    
    // Add mouse interaction variables
    private bool isMouseDown = false;
    private Point lastMousePosition;
    
    // Add paddle movement constraints
    private const int MIN_PADDLE_X = 20;
    private const int MAX_PADDLE_X = 150;
    private const int INITIAL_PADDLE_X = 50;
    
    public PingPongGameDialog()
    {
        // Form settings
        this.ClientSize = new Size(800, 400);
        this.Text = "Ping Pong Game";
        this.DoubleBuffered = true;
        
        // Initialize game objects
        player = new Rectangle(INITIAL_PADDLE_X, Height/2 - 40, 15, 80);
        computer = new Rectangle(ClientSize.Width - 65, Height/2 - 40, 15, 80);
        
        // Initialize balls
        balls = new List<Ball>();
        for (int i = 0; i < BALL_COUNT; i++)
        {
            balls.Add(new Ball(
                new Rectangle(ClientSize.Width/2 - 10, 
                            ClientSize.Height/2 - 10 + (i * 40), // Offset each ball vertically
                            20, 20),
                BASE_SPEED * (i % 2 == 0 ? 1 : -1), // Alternate initial directions
                BASE_SPEED
            ));
        }
        
        // Set up game timer
        gameTimer = new Timer();
        gameTimer.Interval = 16; // Approximately 60 FPS
        gameTimer.Tick += GameTimer_Tick;
        gameTimer.Start();
        
        // Event handlers
        this.Paint += PingPongGameDialog_Paint;
        this.MouseMove += PingPongGameDialog_MouseMove;
        this.MouseDown += PingPongGameDialog_MouseDown;
        this.MouseUp += PingPongGameDialog_MouseUp;
    }
    
    private void PingPongGameDialog_Paint(object sender, PaintEventArgs e)
    {
        using (SolidBrush brush = new SolidBrush(Color.White))
        {
            // Set background
            e.Graphics.Clear(Color.Black);
            
            // Draw paddles
            e.Graphics.FillRectangle(brush, player);
            e.Graphics.FillRectangle(brush, computer);
            
            // Draw all balls
            foreach (var ball in balls)
            {
                e.Graphics.FillRectangle(brush, ball.Rect);
            }
            
            // Draw scores
            e.Graphics.DrawString(playerScore.ToString(), new Font("Arial", 16), brush, ClientSize.Width/4, 20);
            e.Graphics.DrawString(computerScore.ToString(), new Font("Arial", 16), brush, 3*ClientSize.Width/4, 20);
        }
    }
    
    private float Clamp(float value, float min, float max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
    
    private void GameTimer_Tick(object sender, EventArgs e)
    {
        // Handle ball movements and collisions
        for (int i = 0; i < balls.Count; i++)
        {
            var ball = balls[i];
            
            // Move ball
            ball.Rect.X += (int)ball.SpeedX;
            ball.Rect.Y += (int)ball.SpeedY;
            
            // Ball collision with top and bottom
            if (ball.Rect.Y <= 0 || ball.Rect.Y >= ClientSize.Height - ball.Rect.Height)
            {
                ball.SpeedY = -ball.SpeedY * 0.9f;
            }
            
            // Check for paddle collisions
            if (ball.Rect.IntersectsWith(player))
            {
                if (!isMouseDown)
                {
                    ball.SpeedX = Math.Abs(ball.SpeedX) * SPEED_INCREASE;
                    float hitPosition = (ball.Rect.Y + ball.Rect.Height/2) - (player.Y + player.Height/2);
                    ball.SpeedY += hitPosition * 0.1f;
                }
            }
            else if (ball.Rect.IntersectsWith(computer))
            {
                ball.SpeedX = -Math.Abs(ball.SpeedX);
            }
            
            // Check collisions with other balls
            for (int j = i + 1; j < balls.Count; j++)
            {
                var otherBall = balls[j];
                if (ball.Rect.IntersectsWith(otherBall.Rect))
                {
                    // Calculate collision response
                    HandleBallCollision(ball, otherBall);
                }
            }
            
            // Clamp ball speeds
            ball.SpeedX = Clamp(ball.SpeedX, -MAX_SPEED, MAX_SPEED);
            ball.SpeedY = Clamp(ball.SpeedY, -MAX_SPEED, MAX_SPEED);
        }
        
        // Move computer paddle towards the nearest ball that's moving towards it
        Ball targetBall = GetMostThreateningBall();
        if (targetBall != null)
        {
            if (computer.Y + (computer.Height / 2) < targetBall.Rect.Y)
            {
                computer.Y += 5;
            }
            if (computer.Y + (computer.Height / 2) > targetBall.Rect.Y)
            {
                computer.Y -= 5;
            }
        }
        
        // Keep computer paddle in bounds
        if (computer.Y < 0) computer.Y = 0;
        if (computer.Y > ClientSize.Height - computer.Height) 
            computer.Y = ClientSize.Height - computer.Height;
        
        // Check for scoring
        CheckForScoring();
        
        this.Invalidate();
    }

    private Ball GetMostThreateningBall()
    {
        return balls
            .Where(b => b.SpeedX > 0) // Only consider balls moving towards computer
            .OrderBy(b => ClientSize.Width - b.Rect.X) // Get closest ball
            .FirstOrDefault();
    }

    private void CheckForScoring()
    {
        // Create a list to store balls that need to be reset
        List<Ball> ballsToReset = new List<Ball>();
        
        foreach (var ball in balls)
        {
            if (ball.Rect.X <= 0)
            {
                computerScore++;
                ballsToReset.Add(ball);
            }
            else if (ball.Rect.X >= ClientSize.Width - ball.Rect.Width)
            {
                playerScore++;
                ballsToReset.Add(ball);
            }
        }
        
        // Reset only the balls that went out of bounds
        foreach (var ball in ballsToReset)
        {
            ResetSingleBall(ball);
        }
    }

    private void ResetSingleBall(Ball ball)
    {
        Random rand = new Random();
        ball.Rect.X = ClientSize.Width/2 - 10;
        ball.Rect.Y = ClientSize.Height/2 - 10 + rand.Next(-40, 41); // Random vertical position
        ball.SpeedX = rand.Next(0, 2) == 0 ? BASE_SPEED : -BASE_SPEED;
        ball.SpeedY = rand.Next(-3, 4);
    }

    private void ResetBalls()
    {
        // This is now only used for initial setup
        balls.Clear();
        for (int i = 0; i < BALL_COUNT; i++)
        {
            Random rand = new Random();
            balls.Add(new Ball(
                new Rectangle(ClientSize.Width/2 - 10,
                            ClientSize.Height/2 - 10 + (i * 40),
                            20, 20),
                rand.Next(0, 2) == 0 ? BASE_SPEED : -BASE_SPEED,
                rand.Next(-3, 4)
            ));
        }
        
        // Reset player paddle X position
        player.X = INITIAL_PADDLE_X;
    }

    private void PingPongGameDialog_MouseMove(object sender, MouseEventArgs e)
    {
        // Track mouse movement for paddle and potential "smash"
        Point currentPosition = e.Location;
        
        // Move player paddle with mouse (both X and Y)
        player.Y = Math.Min(Math.Max(e.Y - player.Height/2, 0), 
                           ClientSize.Height - player.Height);
        
        // Add horizontal movement within constraints
        player.X = Math.Min(Math.Max(e.X - player.Width/2, MIN_PADDLE_X), 
                           MAX_PADDLE_X);

        // Update ball hit detection for multiple balls
        if (isMouseDown)
        {
            foreach (var ball in balls)
            {
                if (ball.Rect.IntersectsWith(player))
                {
                    float mouseSpeedY = (currentPosition.Y - lastMousePosition.Y);
                    float mouseSpeedX = (currentPosition.X - lastMousePosition.X);
                    
                    ball.SpeedY += mouseSpeedY * 0.2f;
                    ball.SpeedX = Math.Abs(ball.SpeedX) * SPEED_INCREASE;
                    
                    if (Math.Abs(mouseSpeedX) > 5)
                    {
                        ball.SpeedX += Math.Sign(mouseSpeedX) * 2;
                    }
                    
                    ball.SpeedY = Clamp(ball.SpeedY, -MAX_SPEED, MAX_SPEED);
                    ball.SpeedX = Clamp(ball.SpeedX, -MAX_SPEED, MAX_SPEED);
                }
            }
        }
        
        lastMousePosition = currentPosition;
    }
    
    private void PingPongGameDialog_MouseDown(object sender, MouseEventArgs e)
    {
        isMouseDown = true;
        lastMousePosition = e.Location;
    }

    private void PingPongGameDialog_MouseUp(object sender, MouseEventArgs e)
    {
        isMouseDown = false;
    }

    private void HandleBallCollision(Ball ball1, Ball ball2)
    {
        // Calculate center points
        float cx1 = ball1.Rect.X + ball1.Rect.Width / 2f;
        float cy1 = ball1.Rect.Y + ball1.Rect.Height / 2f;
        float cx2 = ball2.Rect.X + ball2.Rect.Width / 2f;
        float cy2 = ball2.Rect.Y + ball2.Rect.Height / 2f;

        // Calculate collision normal
        float dx = cx2 - cx1;
        float dy = cy2 - cy1;
        float distance = (float)Math.Sqrt(dx * dx + dy * dy);
        
        if (distance == 0) return; // Avoid division by zero

        dx /= distance;
        dy /= distance;

        // Calculate relative velocity
        float rvx = ball2.SpeedX - ball1.SpeedX;
        float rvy = ball2.SpeedY - ball1.SpeedY;

        // Calculate velocity along the normal
        float normalVelocity = rvx * dx + rvy * dy;

        // Don't collide if balls are moving apart
        if (normalVelocity > 0) return;

        // Calculate collision impulse
        float restitution = 0.85f; // Bounce factor (0.85 = slight energy loss)
        float impulse = -(1 + restitution) * normalVelocity / 2;

        // Apply impulse to both balls
        ball1.SpeedX -= impulse * dx;
        ball1.SpeedY -= impulse * dy;
        ball2.SpeedX += impulse * dx;
        ball2.SpeedY += impulse * dy;

        // Separate balls to prevent sticking
        float overlap = (ball1.Rect.Width - distance) / 2;
        ball1.Rect.X -= (int)(overlap * dx);
        ball1.Rect.Y -= (int)(overlap * dy);
        ball2.Rect.X += (int)(overlap * dx);
        ball2.Rect.Y += (int)(overlap * dy);
    }
} 
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class CutTheWoodGameDialog : Form
{
    private Timer gameTimer;
    private Player player;
    private List<Tree> trees;
    private List<Hazard> hazards;
    private int score = 0;
    private int level = 1;
    private int treesNeeded;
    private int treesChopped;
    private float timeLeft = 30;
    
    // Game constants
    private const int GROUND_LEVEL = 500;
    private const float TIME_PER_LEVEL = 30f;
    
    private class Player
    {
        public Point Position;
        public bool FacingRight;
        public bool IsChopping;
        public Rectangle HitBox;
        
        public Player()
        {
            Position = new Point(200, GROUND_LEVEL - 100);
            FacingRight = true;
            IsChopping = false;
            UpdateHitBox();
        }
        
        public void UpdateHitBox()
        {
            HitBox = new Rectangle(Position.X, Position.Y, 60, 100);
        }
    }
    
    private class Tree
    {
        public Point Position;
        public int Health;
        public Rectangle HitBox;
        
        public Tree(Point position)
        {
            Position = position;
            Health = 3;
            HitBox = new Rectangle(Position.X, Position.Y, 80, 300);
        }
    }
    
    private class Hazard
    {
        public PointF Position;
        public float VelocityX;
        public float VelocityY;
        public Rectangle HitBox;
        public bool IsBeehive;
        
        public Hazard(Point start, bool isBeehive)
        {
            Position = start;
            IsBeehive = isBeehive;
            VelocityX = isBeehive ? -3 : 0;
            VelocityY = isBeehive ? -2 : 5;
            UpdateHitBox();
        }
        
        public void UpdateHitBox()
        {
            HitBox = new Rectangle(
                (int)Position.X, 
                (int)Position.Y, 
                IsBeehive ? 40 : 30, 
                IsBeehive ? 40 : 30
            );
        }
    }
    
    public CutTheWoodGameDialog()
    {
        this.ClientSize = new Size(800, 600);
        this.Text = "Timber!";
        this.DoubleBuffered = true;
        
        InitializeGame();
        
        // Set up game timer
        gameTimer = new Timer();
        gameTimer.Interval = 16; // ~60 FPS
        gameTimer.Tick += GameTimer_Tick;
        gameTimer.Start();
        
        // Add event handlers
        this.Paint += CutTheWoodGameDialog_Paint;
        this.KeyDown += CutTheWoodGameDialog_KeyDown;
        this.KeyUp += CutTheWoodGameDialog_KeyUp;
    }
    
    private void InitializeGame()
    {
        player = new Player();
        trees = new List<Tree>();
        hazards = new List<Hazard>();
        score = 0;
        level = 1;
        treesNeeded = 5 + level * 2;
        treesChopped = 0;
        timeLeft = TIME_PER_LEVEL;
        
        // Add initial trees
        AddTree();
    }
    
    private void AddTree()
    {
        Random rand = new Random();
        int x = rand.Next(100, 700);
        trees.Add(new Tree(new Point(x, GROUND_LEVEL - 300)));
    }
    
    private void AddHazard()
    {
        Random rand = new Random();
        bool isBeehive = rand.Next(2) == 0;
        Point start = new Point(
            isBeehive ? ClientSize.Width : rand.Next(100, 700),
            isBeehive ? rand.Next(100, 300) : -50
        );
        hazards.Add(new Hazard(start, isBeehive));
    }
    
    private void GameTimer_Tick(object sender, EventArgs e)
    {
        timeLeft -= 0.016f;
        
        // Update hazards
        for (int i = hazards.Count - 1; i >= 0; i--)
        {
            var hazard = hazards[i];
            hazard.Position.X += hazard.VelocityX;
            hazard.Position.Y += hazard.VelocityY;
            hazard.UpdateHitBox();
            
            // Remove off-screen hazards
            if (hazard.Position.Y > ClientSize.Height || 
                hazard.Position.X < -50 || 
                hazard.Position.X > ClientSize.Width)
            {
                hazards.RemoveAt(i);
            }
            
            // Check collision with player
            if (hazard.HitBox.IntersectsWith(player.HitBox))
            {
                GameOver();
                return;
            }
        }
        
        // Randomly add hazards
        if (new Random().Next(100) < 2)
        {
            AddHazard();
        }
        
        // Check for level completion
        if (treesChopped >= treesNeeded)
        {
            level++;
            treesNeeded = 5 + level * 2;
            treesChopped = 0;
            timeLeft = TIME_PER_LEVEL;
        }
        
        // Check for time out
        if (timeLeft <= 0)
        {
            GameOver();
            return;
        }
        
        // Ensure there's always at least one tree
        if (trees.Count == 0)
        {
            AddTree();
        }
        
        this.Invalidate();
    }
    
    private void GameOver()
    {
        gameTimer.Stop();
        MessageBox.Show($"Game Over!\nScore: {score}\nLevel: {level}", "Game Over");
        InitializeGame();
        gameTimer.Start();
    }
    
    private void CutTheWoodGameDialog_Paint(object sender, PaintEventArgs e)
    {
        // Draw ground
        e.Graphics.FillRectangle(Brushes.SaddleBrown, 0, GROUND_LEVEL, ClientSize.Width, ClientSize.Height - GROUND_LEVEL);
        
        // Draw trees
        foreach (var tree in trees)
        {
            DrawTree(e.Graphics, tree);
        }
        
        // Draw hazards
        foreach (var hazard in hazards)
        {
            DrawHazard(e.Graphics, hazard);
        }
        
        // Draw player
        DrawPlayer(e.Graphics);
        
        // Draw UI
        using (Font font = new Font("Arial", 16))
        {
            e.Graphics.DrawString($"Score: {score}", font, Brushes.White, 10, 10);
            e.Graphics.DrawString($"Level: {level}", font, Brushes.White, 10, 40);
            e.Graphics.DrawString($"Trees: {treesChopped}/{treesNeeded}", font, Brushes.White, 10, 70);
            e.Graphics.DrawString($"Time: {(int)timeLeft}", font, Brushes.White, 10, 100);
        }
    }
    
    private void DrawTree(Graphics g, Tree tree)
    {
        // Draw trunk
        g.FillRectangle(Brushes.SaddleBrown, 
            tree.Position.X, tree.Position.Y, 
            80, 300);
        
        // Draw leaves
        g.FillEllipse(Brushes.ForestGreen,
            tree.Position.X - 20, tree.Position.Y - 60,
            120, 120);
        
        // Draw damage indicator
        for (int i = 0; i < tree.Health; i++)
        {
            g.FillRectangle(Brushes.Red,
                tree.Position.X + 20 + (i * 20),
                tree.Position.Y + 150,
                10, 10);
        }
    }
    
    private void DrawHazard(Graphics g, Hazard hazard)
    {
        if (hazard.IsBeehive)
        {
            g.FillEllipse(Brushes.Yellow, hazard.HitBox);
        }
        else
        {
            g.FillRectangle(Brushes.Gray, hazard.HitBox);
        }
    }
    
    private void DrawPlayer(Graphics g)
    {
        // Simple player representation
        g.FillRectangle(Brushes.Blue, player.HitBox);
        
        // Draw axe when chopping
        if (player.IsChopping)
        {
            Point axePos = player.FacingRight ? 
                new Point(player.Position.X + 50, player.Position.Y + 30) :
                new Point(player.Position.X - 20, player.Position.Y + 30);
                
            g.FillRectangle(Brushes.Silver, axePos.X, axePos.Y, 30, 10);
        }
    }
    
    private void CutTheWoodGameDialog_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Left:
                player.Position.X -= 5;
                player.FacingRight = false;
                break;
            case Keys.Right:
                player.Position.X += 5;
                player.FacingRight = true;
                break;
            case Keys.Space:
                if (!player.IsChopping)
                {
                    player.IsChopping = true;
                    ChopTree();
                }
                break;
        }
        
        player.UpdateHitBox();
        this.Invalidate();
    }
    
    private void CutTheWoodGameDialog_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space)
        {
            player.IsChopping = false;
        }
    }
    
    private void ChopTree()
    {
        foreach (var tree in trees.ToArray())
        {
            if (Math.Abs(player.Position.X - tree.Position.X) < 100)
            {
                tree.Health--;
                if (tree.Health <= 0)
                {
                    trees.Remove(tree);
                    score += 100;
                    treesChopped++;
                }
                break;
            }
        }
    }
} 
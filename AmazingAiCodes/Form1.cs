using System;
using System.Drawing;
using System.Windows.Forms;

namespace AmazingAiCodes
{
    public partial class Form1 : Form
    {
        // Constants for button layout
        private const int BUTTON_WIDTH = 200;
        private const int BUTTON_HEIGHT = 40;
        private const int BUTTON_MARGIN = 10;
        private const int BUTTONS_PER_ROW = 3;

        private int currentButtonCount = 0;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Amazing AI Games";
            
            // Set a reasonable starting size for the form
            this.ClientSize = new Size(
                (BUTTON_WIDTH + BUTTON_MARGIN) * BUTTONS_PER_ROW + BUTTON_MARGIN,
                400);
            
            // Add the Ping Pong game button
            AddGameButton("Ping Pong Game", OpenPingPongGame);
            AddGameButton("Cut The Wood", OpenCutTheWoodGame);
        }

        private void AddGameButton(string text, EventHandler clickHandler)
        {
            // Calculate button position
            int row = currentButtonCount / BUTTONS_PER_ROW;
            int col = currentButtonCount % BUTTONS_PER_ROW;

            Button btn = new Button
            {
                Text = text,
                Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT),
                Location = new Point(
                    BUTTON_MARGIN + (BUTTON_WIDTH + BUTTON_MARGIN) * col,
                    BUTTON_MARGIN + (BUTTON_HEIGHT + BUTTON_MARGIN) * row
                ),
                Font = new Font("Arial", 12),
                BackColor = Color.LightBlue,
                FlatStyle = FlatStyle.Flat
            };

            btn.Click += clickHandler;
            this.Controls.Add(btn);
            
            currentButtonCount++;
        }

        private void OpenPingPongGame(object sender, EventArgs e)
        {
            var gameDialog = new PingPongGameDialog();
            gameDialog.ShowDialog();
        }

        private void OpenCutTheWoodGame(object sender, EventArgs e)
        {
            var gameDialog = new CutTheWoodGameDialog();
            gameDialog.ShowDialog();
        }
    }
}

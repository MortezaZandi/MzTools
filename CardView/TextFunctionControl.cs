using System;
using System.Drawing;
using System.Windows.Forms;

namespace NodeEditor
{
    public partial class TextFunctionControl : UserControl
    {
        public Label DisplayLabel { get; private set; }

        public TextFunctionControl()
        {
            InitializeComponent();
            InitializeControl();
        }

        private void InitializeControl()
        {
            DisplayLabel = new Label
            {
                AutoSize = false,
                Size = new Size(100, 25),
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10f),
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(5, 5) // Adjusted position to avoid overlap
            };

            this.Controls.Add(DisplayLabel);
            this.Size = new Size(120, 40); // Set size as needed
        }

        public void UpdateText(string text, float fontSize, Color color)
        {
            DisplayLabel.Text = text;
            DisplayLabel.Font = new Font("Segoe UI", fontSize);
            DisplayLabel.ForeColor = color;

            // Adjust label size based on content
            var textSize = TextRenderer.MeasureText(text, DisplayLabel.Font);
            DisplayLabel.Size = new Size(
                Math.Max(100, textSize.Width + 20),
                Math.Max(25, textSize.Height + 10)
            );
        }
    }
} 
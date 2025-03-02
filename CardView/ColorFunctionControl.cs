using System;
using System.Drawing;
using System.Windows.Forms;

namespace NodeEditor
{
    public partial class ColorFunctionControl : UserControl
    {
        public Button ColorButton { get; private set; }
        private Color currentColor = Color.White;

        public ColorFunctionControl()
        {
            InitializeComponent();
            InitializeControl();
        }

        private void InitializeControl()
        {
            ColorButton = new Button
            {
                Size = new Size(60, 25),
                BackColor = currentColor,
                Text = "Select Color",
                FlatStyle = FlatStyle.Flat,
                Location = new Point(5, 5) // Adjust as needed
            };

            ColorButton.Click += ColorButton_Click;

            this.Controls.Add(ColorButton);
            this.Size = new Size(70, 35); // Set size as needed
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog())
            {
                colorDialog.Color = currentColor;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    currentColor = colorDialog.Color;
                    ColorButton.BackColor = currentColor;
                }
            }
        }

        public Color SelectedColor => currentColor; // Expose the selected color
    }
} 
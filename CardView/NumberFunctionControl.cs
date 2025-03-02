using System;
using System.Drawing;
using System.Windows.Forms;

namespace NodeEditor
{
    public partial class NumberFunctionControl : UserControl
    {
        private float currentValue = 0;

        public float Value
        {
            get => currentValue;
            set
            {
                currentValue = value;
                ValueTextBox.Text = currentValue.ToString();
            }
        }

        public TextBox ValueTextBox { get; private set; }

        public NumberFunctionControl()
        {
            InitializeComponent();
            InitializeControl();
        }

        private void InitializeControl()
        {
            ValueTextBox = new TextBox
            {
                Width = 60,
                Location = new Point(5, 30) // Adjusted position to avoid overlap
            };

            ValueTextBox.TextChanged += (s, e) =>
            {
                if (float.TryParse(ValueTextBox.Text, out float value))
                {
                    Value = value; // Update the value
                }
            };

            this.Controls.Add(ValueTextBox);
            this.Size = new Size(120, 60); // Set size as needed
        }
    }
} 
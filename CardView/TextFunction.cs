using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NodeEditor
{
    public class TextFunction : IFunction
    {
        private Dictionary<string, IFunctionPort> inputs;
        private Dictionary<string, IFunctionPort> outputs;
        private TextFunctionControl control;

        public string Name => "Text Display";
        public Dictionary<string, IFunctionPort> Inputs => inputs;
        public Dictionary<string, IFunctionPort> Outputs => outputs;

        public TextFunction()
        {
            inputs = new Dictionary<string, IFunctionPort>
            {
                ["Value"] = new FunctionPort("Value", typeof(object), true, this),
                ["Size"] = new FunctionPort("Size", typeof(float), true, this),
                ["Color"] = new FunctionPort("Color", typeof(Color), true, this)
            };

            outputs = new Dictionary<string, IFunctionPort>();

            control = new TextFunctionControl();
        }

        public void Calculate()
        {
            // Get the main value
            var value = inputs["Value"].Value;
            string displayText = value?.ToString() ?? "---";

            // Get the font size (optional)
            float fontSize = 10f;
            if (inputs["Size"].Value is float size)
            {
                fontSize = Math.Max(6f, Math.Min(size, 72f)); // Limit size between 6 and 72
            }

            // Get the text color (optional)
            Color textColor = Color.White;
            if (inputs["Color"].Value is Color color)
            {
                textColor = color;
            }

            // Update the label in the control
            control.UpdateText(displayText, fontSize, textColor);
        }

        public UserControl GetControl()
        {
            return control; // Return the UserControl
        }
    }
} 
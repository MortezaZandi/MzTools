using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NodeEditor
{
    public class ColorFunction : IFunction
    {
        private Dictionary<string, IFunctionPort> inputs;
        private Dictionary<string, IFunctionPort> outputs;
        private ColorFunctionControl control;

        public string Name => "Color";
        public Dictionary<string, IFunctionPort> Inputs => inputs;
        public Dictionary<string, IFunctionPort> Outputs => outputs;

        public ColorFunction()
        {
            inputs = new Dictionary<string, IFunctionPort>();
            outputs = new Dictionary<string, IFunctionPort>
            {
                ["Color"] = new FunctionPort("Color", typeof(Color), false, this)
            };

            control = new ColorFunctionControl();
            control.ColorButton.Click += (s, e) =>
            {
                outputs["Color"].Value = control.SelectedColor; // Update output when color is selected
                Calculate(); // Call Calculate to ensure output is updated
            };
        }

        public void Calculate()
        {
            // Update the output value based on the selected color
            outputs["Color"].Value = control.SelectedColor;
        }

        public UserControl GetControl()
        {
            return control; // Return the UserControl
        }
    }
} 
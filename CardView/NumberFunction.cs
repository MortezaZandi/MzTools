using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NodeEditor
{
    public class NumberFunction : IFunction
    {
        private Dictionary<string, IFunctionPort> inputs;
        private Dictionary<string, IFunctionPort> outputs;
        private NumberFunctionControl control;

        public string Name => "Number";
        public Dictionary<string, IFunctionPort> Inputs => inputs;
        public Dictionary<string, IFunctionPort> Outputs => outputs;

        public NumberFunction()
        {
            inputs = new Dictionary<string, IFunctionPort>();
            outputs = new Dictionary<string, IFunctionPort>
            {
                ["Value"] = new FunctionPort("Value", typeof(float), false, this)
            };

            control = new NumberFunctionControl();
            control.ValueTextBox.TextChanged += (s, e) =>
            {
                if (float.TryParse(control.ValueTextBox.Text, out float value))
                {
                    Value = value; // Update the value
                }
            };
        }

        public float Value
        {
            get => control.Value;
            set
            {
                control.Value = value;
                outputs["Value"].Value = value; // Update output value
            }
        }

        public void Calculate()
        {
            outputs["Value"].Value = control.Value; // Update output value
        }

        public UserControl GetControl()
        {
            return control; // Return the UserControl
        }
    }
} 
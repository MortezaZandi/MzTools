using System;
using System.Collections.Generic;

namespace NodeEditor
{
    public class ToStringFunction : IFunction
    {
        private Dictionary<string, IFunctionPort> inputs;
        private Dictionary<string, IFunctionPort> outputs;

        public string Name => "To String";
        public Dictionary<string, IFunctionPort> Inputs => inputs;
        public Dictionary<string, IFunctionPort> Outputs => outputs;

        public ToStringFunction()
        {
            inputs = new Dictionary<string, IFunctionPort>
            {
                ["Input"] = new FunctionPort("Input", typeof(object), true, this)
            };

            outputs = new Dictionary<string, IFunctionPort>
            {
                ["Output"] = new FunctionPort("Output", typeof(string), false, this)
            };
        }

        public void Calculate()
        {
            var inputValue = inputs["Input"].Value;
            outputs["Output"].Value = inputValue?.ToString() ?? "null"; // Convert to string
        }
    }
} 
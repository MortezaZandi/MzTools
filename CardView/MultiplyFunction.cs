using System;
using System.Collections.Generic;

namespace NodeEditor
{
    public class MultiplyFunction : IFunction
    {
        private Dictionary<string, IFunctionPort> inputs;
        private Dictionary<string, IFunctionPort> outputs;

        public string Name => "Multiply";
        public Dictionary<string, IFunctionPort> Inputs => inputs;
        public Dictionary<string, IFunctionPort> Outputs => outputs;

        public MultiplyFunction()
        {
            inputs = new Dictionary<string, IFunctionPort>
            {
                ["A"] = new FunctionPort("A", typeof(float), true, this),
                ["B"] = new FunctionPort("B", typeof(float), true, this)
            };

            outputs = new Dictionary<string, IFunctionPort>
            {
                ["Result"] = new FunctionPort("Result", typeof(float), false, this)
            };
        }

        public void Calculate()
        {
            float a = Convert.ToSingle(inputs["A"].Value ?? 0f);
            float b = Convert.ToSingle(inputs["B"].Value ?? 0f);
            outputs["Result"].Value = a * b;
        }
    }
} 
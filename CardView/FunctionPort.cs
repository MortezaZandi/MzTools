using System;
using System.Collections.Generic;

namespace NodeEditor
{
    public class FunctionPort : IFunctionPort
    {
        private List<IFunctionPort> connections = new List<IFunctionPort>();

        public string Name { get; }
        public Type DataType { get; }
        public bool IsInput { get; }
        public IFunction ParentFunction { get; }
        public List<IFunctionPort> Connections => connections;
        public object Value { get; set; }

        public FunctionPort(string name, Type dataType, bool isInput, IFunction parentFunction)
        {
            Name = name;
            DataType = dataType;
            IsInput = isInput;
            ParentFunction = parentFunction;
        }
    }
} 
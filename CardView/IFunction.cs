using System;
using System.Collections.Generic;

namespace NodeEditor
{
    public interface IFunction
    {
        string Name { get; }
        Dictionary<string, IFunctionPort> Inputs { get; }
        Dictionary<string, IFunctionPort> Outputs { get; }
        void Calculate();
    }

    public interface IFunctionPort
    {
        string Name { get; }
        Type DataType { get; }
        object Value { get; set; }
        bool IsInput { get; }
        List<IFunctionPort> Connections { get; }
        IFunction ParentFunction { get; }
    }
} 
using System.Collections.ObjectModel;       
using Core.Runtime;       

namespace Core.Editor.Nodes
{
    public abstract class IntermediateNode : CodeFunctionNode
    {
        public override float Value { get; }  
        public abstract void RemoveChild(CodeFunctionNode child, string portName);
        public abstract void AddChild(CodeFunctionNode child, string portName);
        public abstract ReadOnlyCollection<CodeFunctionNode> Children { get; }
    }
}
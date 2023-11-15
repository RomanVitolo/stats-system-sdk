using Core.Scripts.Runtime.Nodes;
using UnityEngine;

namespace Core.Editor.Nodes
{
    [NodeType(typeof(MultiplyNode))]
    [Title("Math", "Multiply")]
    public class MultiplyNodeView : NodeView
    {
        public MultiplyNodeView()
        {
            title = "Multiply";
            Node = ScriptableObject.CreateInstance<MultiplyNode>();
            output = CreateOutputPort();
            inputs.Add(CreateInputPort("A"));
            inputs.Add(CreateInputPort("B"));
        } 
    }
}
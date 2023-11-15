using Core.Scripts.Runtime.Nodes;
using UnityEngine;

namespace Core.Editor.Nodes
{
    [NodeType(typeof(DivideNode))]
    [Title("Math", "Divide")]
    public class DivideNodeView : NodeView
    {
        public DivideNodeView()
        {
            title = "Divide";
            Node = ScriptableObject.CreateInstance<DivideNode>();
            output = CreateOutputPort();
            inputs.Add(CreateInputPort("A"));
            inputs.Add(CreateInputPort("B"));    
        }
    }
}
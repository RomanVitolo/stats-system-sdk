using Core.Scripts.Runtime.Nodes;
using UnityEngine;

namespace Core.Editor.Nodes
{
    [NodeType(typeof(SubtractNode))]
    [Title("Math", "Subtract")]
    public class SubtractNodeView : NodeView
    {
        public SubtractNodeView()
        {
            title = "Subtract";
            Node = ScriptableObject.CreateInstance<SubtractNode>();
            output = CreateOutputPort();
            inputs.Add(CreateInputPort("A"));
            inputs.Add(CreateInputPort("B"));
        }
    }
}
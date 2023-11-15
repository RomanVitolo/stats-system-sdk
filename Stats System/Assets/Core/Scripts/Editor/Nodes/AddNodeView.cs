using Core.Scripts.Runtime.Nodes;
using UnityEngine;

namespace Core.Editor.Nodes
{
    [NodeType(typeof(AddNode))]
    [Title("Math", "Add")]
    public class AddNodeView : NodeView
    {
        public AddNodeView()
        {
            title = "Add";
            Node = ScriptableObject.CreateInstance<AddNode>();
            output = CreateOutputPort();
            inputs.Add(CreateInputPort("A"));
            inputs.Add(CreateInputPort("B"));
        }
    }
}
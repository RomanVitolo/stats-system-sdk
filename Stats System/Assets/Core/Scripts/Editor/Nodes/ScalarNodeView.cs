using Core.Runtime;
using UnityEngine;

namespace Core.Editor.Nodes
{
    [NodeType(typeof(ScalarNode))]
    [Title("Math", "Scalar")]
    public class ScalarNodeView : NodeView
    {
        public ScalarNodeView()
        {
            title = "Scalar";
            Node = ScriptableObject.CreateInstance<ScalarNode>();
            output = CreateOutputPort();
        }
    }
}
using UnityEngine;

namespace Core.Runtime
{
    public class ResultNode : CodeFunctionNode
    {
        [HideInInspector] public CodeFunctionNode Child;        
        public override float Value => Child.Value;
    }
}
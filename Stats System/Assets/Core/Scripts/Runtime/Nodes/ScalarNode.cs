using UnityEngine;

namespace Core.Runtime
{
    public class ScalarNode : CodeFunctionNode
    {
        [SerializeField] protected float m_Value;
        public override float Value => m_Value;
    }
}
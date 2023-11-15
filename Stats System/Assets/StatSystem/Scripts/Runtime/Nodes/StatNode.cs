using Core.Runtime;
using UnityEngine;

namespace StatSystem.Nodes
{
    public class StatNode : CodeFunctionNode
    {
        [SerializeField] private string m_StatName;
        public string StatName => m_StatName;
        public Stat Stat;
        public override float Value => Stat.Value;
    }
}
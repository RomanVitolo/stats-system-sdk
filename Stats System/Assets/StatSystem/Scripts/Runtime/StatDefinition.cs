using Core;
using UnityEngine;

namespace StatSystem
{
    [CreateAssetMenu(fileName = "StatDefinition", menuName = "StatSystem/StatDefinition", order = 0)]
    public class StatDefinition : ScriptableObject
    {
        [SerializeField] private int m_baseValue;
        [SerializeField] private int m_cap = -1;
        [SerializeField] private NodeGraph m_formula;
        public int baseValue => m_baseValue;
        public int cap => m_cap;
        public NodeGraph Formula => m_formula; 
      
    }
}
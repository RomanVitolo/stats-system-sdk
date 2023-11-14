using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace StatSystem
{
    [CreateAssetMenu(fileName = "StatDatabase", menuName = "StatSystem/StatDatabase", order = 0)]
    public class StatDatabase : ScriptableObject
    {
        public List<StatDefinition> Stats;
        public List<StatDefinition> Attributes;
        public List<StatDefinition> PrimaryStats;
        
    }
}
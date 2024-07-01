using System;
using System.Collections.Generic;
using StatSystem.Nodes;
using UnityEngine;

namespace StatSystem
{
    public class StatController : MonoBehaviour
    {
        [SerializeField] private StatDatabase m_StatDatabase;
        protected Dictionary<string, Stat> m_Stats = new Dictionary<string, Stat>(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, Stat> Stats => m_Stats;

        private bool m_IsInitialized;
        public bool IsInitialized => m_IsInitialized;
        public event Action Initialized;
        public event Action WillUnitialize;

        protected virtual void Awake()
        {
            if (!m_IsInitialized)
            {
                Initialize();     
            }                
        }

        private void OnDestroy()
        {
            WillUnitialize?.Invoke();
        }

        protected void Initialize()
        {
            foreach (StatDefinition definition in m_StatDatabase.Stats)
            {
                m_Stats.Add(definition.name, new Stat(definition));
            }

            foreach (StatDefinition definition in m_StatDatabase.Attributes)
            {
               m_Stats.Add(definition.name, new Attribute(definition)); 
            }

            foreach (StatDefinition definition in m_StatDatabase.PrimaryStats)
            {
               m_Stats.Add(definition.name, new PrimaryStat(definition)); 
            }
            
            InitializeStatFormulas();

            foreach (Stat stat in m_Stats.Values)
            {
                stat.Initialize();
            }
            
            m_IsInitialized = true;
            Initialized?.Invoke();
        }

        protected virtual void InitializeStatFormulas()
        {
            foreach (Stat currentStat in m_Stats.Values)
            {
                if (currentStat.Definition.Formula != null && currentStat.Definition.Formula.RootNode != null)
                {
                    List<StatNode> statNodes = currentStat.Definition.Formula.FindNodesOfType<StatNode>();

                    foreach (StatNode statNode in statNodes)
                    {
                        if (m_Stats.TryGetValue(statNode.StatName.Trim(), out Stat stat))
                        {
                            statNode.Stat = stat;
                            stat.ValueChanged += currentStat.CalculateValue;
                        }
                        else
                        {
                            Debug.LogWarning($"Stat {statNode.StatName.Trim()} does not exist!");
                        }
                    }
                }  
            }
        }
    }
}
                
using System;
using System.Collections.Generic;
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

        protected void Awake()
        {
            if (!m_IsInitialized)
            {
                Initialize();
                m_IsInitialized = true;
                Initialized?.Invoke();
            }        
        }

        private void OnDestroy()
        {
            WillUnitialize?.Invoke();
        }

        private void Initialize()
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
        }
    }
}
                
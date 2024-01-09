using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace StatSystem
{
    public class Stat
    {
        protected StatDefinition m_Definition;
        public StatDefinition Definition => m_Definition;
        protected int m_Value;
        public int Value => m_Value;
        public virtual int BaseValue => m_Definition.baseValue;
        public event Action ValueChanged;
        protected List<StatModifier> m_Modifiers = new List<StatModifier>();

        public Stat(StatDefinition definition)
        {
            m_Definition = definition; 
        }
        
        public void Initialize()
        {
            CalculateValue();
        }

        public void AddModifier(StatModifier statModifier)
        {
            m_Modifiers.Add(statModifier);
            CalculateValue();
        }

        public void RemoveModifierFromSource(Object source)
        {
            m_Modifiers = m_Modifiers.Where(m => m.Source.GetInstanceID() != source.GetInstanceID()).ToList();
            CalculateValue();
        }

        internal void CalculateValue()
        {
            int newValue = BaseValue;

            if (m_Definition.Formula != null && m_Definition.Formula.RootNode != null)
            {
                newValue += Mathf.RoundToInt(m_Definition.Formula.RootNode.Value);
            }
            
            m_Modifiers.Sort((x, y) => x.Type.CompareTo(y.Type));

            for (int i = 0; i < m_Modifiers.Count; i++)
            {
                StatModifier modifier = m_Modifiers[i];
                if (modifier.Type == ModifierOperationType.Additive)
                {
                    newValue += modifier.Magnitude;
                }
                else if (modifier.Type == ModifierOperationType.Multiplicative)
                {
                    newValue *= modifier.Magnitude;
                }
            }

            if (m_Definition.cap >= 0)
            {
                newValue = Mathf.Min(newValue, m_Definition.cap);
            }

            if (m_Value != newValue)
            {
                m_Value = newValue;
                ValueChanged?.Invoke();
            }
        }      
    }
}



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
        protected int m_value;
        public int value => m_value;
        public virtual int baseValue => m_Definition.baseValue;
        public event Action ValueChanged;
        protected List<StatModifier> m_Modifiers = new List<StatModifier>();

        public Stat(StatDefinition definition)
        {
            m_Definition = definition;
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

        protected void CalculateValue()
        {
            int newValue = baseValue;
            
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

            if (m_value != newValue)
            {
                m_value = newValue;
                ValueChanged?.Invoke();
            }
        }
        }
    }



using System;
using UnityEngine;

namespace StatSystem
{
    public class Attribute : Stat
    {
        protected int m_CurrentValue;
        public int currentValue => m_CurrentValue;
        public event Action currentValueChanged;
        public event Action<StatModifier> appliedModifier;
        
        public Attribute(StatDefinition definition) : base(definition)
        {
            m_CurrentValue = value;
        }

        public virtual void ApplyModifier(StatModifier modifier)
        {
            int newValue = m_CurrentValue;
            if (modifier.Type == ModifierOperationType.Override)
                newValue = modifier.Magnitude;
            else if (modifier.Type == ModifierOperationType.Additive)
                newValue += modifier.Magnitude;
            else if (modifier.Type == ModifierOperationType.Multiplicative) newValue *= modifier.Magnitude;

            newValue = Mathf.Clamp(newValue, 0, m_value);

            if (currentValue != newValue)
            {
                m_CurrentValue = newValue;
                currentValueChanged?.Invoke();
                appliedModifier?.Invoke(modifier);
            }
        }
    }
}
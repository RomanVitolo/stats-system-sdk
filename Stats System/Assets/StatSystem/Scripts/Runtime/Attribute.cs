using System;
using SaveSystem.Scripts.Runtime;
using UnityEngine;

namespace StatSystem
{
    public class Attribute : Stat, ISavable
    {
        protected int m_CurrentValue;
        public int currentValue => m_CurrentValue;
        public event Action currentValueChanged;
        public event Action<StatModifier> appliedModifier;
        
        public Attribute(StatDefinition definition) : base(definition)
        {
            m_CurrentValue = Value;            
        }

        public virtual void ApplyModifier(StatModifier modifier)
        {
            int newValue = m_CurrentValue;
            if (modifier.Type == ModifierOperationType.Override)
                newValue = modifier.Magnitude;
            else if (modifier.Type == ModifierOperationType.Additive)
                newValue += modifier.Magnitude;
            else if (modifier.Type == ModifierOperationType.Multiplicative) newValue *= modifier.Magnitude;

            newValue = Mathf.Clamp(newValue, 0, m_Value);

            if (currentValue != newValue)
            {
                m_CurrentValue = newValue;
                currentValueChanged?.Invoke();
                appliedModifier?.Invoke(modifier);
            }
        }

        #region Save System

        public object data => new AttributeData
        {
            currentValue = currentValue
        };
        public void Load(object data)
        {
            AttributeData attributeData = (AttributeData) data;
            m_CurrentValue = attributeData.currentValue;
            currentValueChanged?.Invoke();
        }

        [Serializable]
        protected class AttributeData
        {
            public int currentValue;
        }

        #endregion
       
    }
}
using System;
using System.Runtime.CompilerServices;
using SaveSystem.Scripts.Runtime;
using UnityEngine;

[assembly: InternalsVisibleTo("StatSystem.Tests")]
namespace StatSystem
{
    public class PrimaryStat : Stat, ISavable
    {
        private int m_BaseValue;
        public override int BaseValue => m_BaseValue;

        public PrimaryStat(StatDefinition definition) : base(definition)
        {
            m_BaseValue = definition.baseValue;
            CalculateValue();
        }

        internal void Add(int amount)
        {
            m_BaseValue += amount;
            CalculateValue();
        }

        internal void Subtract(int amount)
        {
            m_BaseValue -= amount;
            CalculateValue();
        }


        #region Stat System

        public object data => new PrimaryStatData
        {
           baseValue = BaseValue
        };
        public void Load(object data)
        {
            PrimaryStatData primaryStatData = (PrimaryStatData) data;
            m_BaseValue = primaryStatData.baseValue;
            CalculateValue();
        }

        [Serializable]
        protected class PrimaryStatData
        {
            public int baseValue;
        }

        #endregion
      
    }
}
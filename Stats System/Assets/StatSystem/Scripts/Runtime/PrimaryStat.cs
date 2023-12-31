﻿using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("StatSystem.Tests")]
namespace StatSystem
{
    public class PrimaryStat : Stat
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
    }
}
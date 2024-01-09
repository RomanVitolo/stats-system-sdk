using System;
using System.Collections.Generic;
using Core;
using LevelSystem.Nodes;
using UnityEngine;

namespace LevelSystem
{
    public class LevelController : MonoBehaviour, ILevelable
    {
        [SerializeField] private int m_Level = 1;
        [SerializeField] private int m_CurrentExperience;
        [SerializeField] private NodeGraph m_RequiredExperienceFormula;

        private bool m_IsInitialized;

        public int Level => m_Level;
        public event Action LevelChanged;
        public event Action CurrentExperienceChanged;

        public int CurrentExperience
        {
            get => m_CurrentExperience;
            set
            {
                if (value >= RequiredExperience)
                {
                    m_CurrentExperience = value - RequiredExperience;
                    CurrentExperienceChanged?.Invoke();
                    m_Level++;
                    LevelChanged?.Invoke();
                } 
                else if (value < RequiredExperience)
                {
                    m_CurrentExperience = value;
                    CurrentExperienceChanged?.Invoke();
                }
            }
        }                                            
        public int RequiredExperience => 
            Mathf.RoundToInt(m_RequiredExperienceFormula.RootNode.Value);
        public bool IsInitialized { get; }
        public event Action Initialized;
        public event Action WillUninitialized;

        private void Awake()
        {
            if (!m_IsInitialized)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            List<LevelNode> levelNodes = 
                m_RequiredExperienceFormula.FindNodesOfType<LevelNode>();
            
            foreach (LevelNode levelNode in levelNodes)
            {
                levelNode.Levelable = this;
            }

            m_IsInitialized = true;
            Initialized?.Invoke();
        }

        private void OnDestroy()
        {
            WillUninitialized?.Invoke();
        }
    }
}
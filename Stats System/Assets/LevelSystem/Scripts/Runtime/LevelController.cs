﻿using System;
using System.Collections.Generic;
using Core;
using LevelSystem.Nodes;
using SaveSystem.Scripts.Runtime;
using UnityEngine;

namespace LevelSystem
{
    public class LevelController : MonoBehaviour, ILevelable, ISavable
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

        public bool IsInitialized => m_IsInitialized;
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

        #region Save System

        public object data => new LevelControllerData
        {
           level = m_Level,
           currentExperience = m_CurrentExperience
        };
        
        public void Load(object data)
        {
            LevelControllerData levelControllerData = (LevelControllerData) data;
            m_CurrentExperience = levelControllerData.currentExperience;
            CurrentExperienceChanged?.Invoke();
            m_Level = levelControllerData.level;
            LevelChanged?.Invoke();
        }

        [Serializable]
        protected class LevelControllerData
        {
            public int level;
            public int currentExperience;
        }

        #endregion
        
       
    }
}
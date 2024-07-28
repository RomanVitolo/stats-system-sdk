using System;
using System.Collections.Generic;
using LevelSystem;
using LevelSystem.Nodes;
using SaveSystem.Scripts.Runtime;
using UnityEngine;

namespace StatSystem
{
    [RequireComponent(typeof(ILevelable))]
    public class PlayerStatController : StatController, ISavable
    {
        protected ILevelable m_Levelable;
        protected int m_StatPoints = 5;
        public event Action StatPointsChanged;

        public int StatPoints
        {
            get => m_StatPoints;
            internal set
            {
                m_StatPoints = value;
                StatPointsChanged?.Invoke();
            }
        }

        protected override void Awake()
        {
            m_Levelable = GetComponent<ILevelable>();
        }

        private void OnEnable()
        {
            m_Levelable.Initialized += OnLevelableInitialized;
            m_Levelable.WillUninitialized += UnregisterEvents;
            if (m_Levelable.IsInitialized)
            {
                OnLevelableInitialized();
            }
        }

        private void OnDisable()
        {
            m_Levelable.Initialized -= OnLevelableInitialized;
            m_Levelable.WillUninitialized -= UnregisterEvents;
            if (m_Levelable.IsInitialized)
            {
                UnregisterEvents();
            }
        }

        private void UnregisterEvents()
        {
            m_Levelable.LevelChanged -= OnLevelChanged;
        }

        private void OnLevelChanged()
        {
            StatPoints += 5;
        }

        private void OnLevelableInitialized()
        {
            Initialize();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            m_Levelable.LevelChanged += OnLevelChanged;
        }

        protected override void InitializeStatFormulas()
        {
            base.InitializeStatFormulas();
            foreach (Stat currentStat in m_Stats.Values)
            {
                if (currentStat.Definition.Formula != null && currentStat.Definition.Formula.RootNode != null)
                {
                    List<LevelNode> levelNodes = currentStat.Definition.Formula.FindNodesOfType<LevelNode>();
                    foreach (LevelNode levelNode in levelNodes)
                    {
                        levelNode.Levelable = m_Levelable;
                        m_Levelable.LevelChanged += currentStat.CalculateValue;
                    }
                }
            }
        }

        #region Stat System

        public object data
        {
            get
            {
                return new PlayerStatControllerData(base.data as StatControllerData)
                {
                    statPoints = m_StatPoints
                };
            }                       
        }

        public void Load(object data)
        {
            base.Load(data);
            PlayerStatControllerData playerStatControllerData = (PlayerStatControllerData) data;
            m_StatPoints = playerStatControllerData.statPoints;
            StatPointsChanged?.Invoke();
        }

        [Serializable]
        protected class PlayerStatControllerData : StatControllerData
        {
            public int statPoints;

            public PlayerStatControllerData(StatControllerData statControllerData)
            {
                stats = statControllerData.stats;
            }
        }

        #endregion
    }
}
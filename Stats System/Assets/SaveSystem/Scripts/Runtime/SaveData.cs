using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

namespace SaveSystem.Scripts.Runtime
{
    [CreateAssetMenu(fileName = "SaveData", menuName = "SaveSystem/Savedata", order = 0)]
    public class SaveData : ScriptableObject
    {
        [SerializeField] private LoadDataChannel m_LoadDataChannel;
        [SerializeField] private SaveDataChannel m_SaveDataChannel;
        [SerializeField] private string m_FileName;
        [HideInInspector, SerializeField] private string m_Path;

        private Dictionary<string, object> m_data = new Dictionary<string, object>();
        public bool previousSaveExist => File.Exists(m_Path);

        [ContextMenu("Delete Save")]
        private void DeleteSave()
        {
            if (previousSaveExist)
            {
                File.Delete(m_Path);
            }
        }

        public void Save(string id, object data)
        {
            m_data[id] = data;
        }

        public void Load(string id, out object data)
        {
            data = m_data[id];
        }

        public void Load()
        {
            FileManager.LoadFromBinaryFile(m_Path, out m_data);
            m_LoadDataChannel.Load();
            m_data.Clear();
        }

        public void Save()
        {
            if(previousSaveExist)
                FileManager.LoadFromBinaryFile(m_Path, out m_data);
            m_SaveDataChannel.Save();
            FileManager.SaveToBinaryFile(m_Path, m_data);
            m_data.Clear();
        }
        
        private void OnValidate()
        {
            m_Path = Path.Combine(Application.persistentDataPath, m_FileName);
        }
    }
}
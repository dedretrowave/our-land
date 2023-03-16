using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Src.DI;
using Src.Levels.Level;
using UnityEngine;

namespace Src.Saves
{
    public class SaveSystem : MonoBehaviour
    {
        [SerializeField] private string _localPathToFile = "player.dat";
        
        private PlayerData _data;
        private string _pathToFile;

        public void SaveLevel(LevelData data)
        {
            _data.LevelDataList.Add(data);
            
            Save();
        }

        public LevelData GetLevelById(int id)
        {
           return _data.LevelDataList.Find(level => level.Id == id);
        }
        
        private void Awake()
        {
            _pathToFile = $"{Application.persistentDataPath}/{_localPathToFile}";
            
            DependencyContext.Dependencies.Add(typeof(SaveSystem), () => this);
            
            LoadInternal();
        }

        private void SaveInternal(string data)
        {
            File.WriteAllText(_pathToFile, data);
        }

        private void LoadInternal()
        {
            if (!File.Exists(_pathToFile))
            {
                File.Create(_pathToFile);
            }

            string serializedData = File.ReadAllText(_pathToFile);
            PlayerData deserializedData = JsonConvert.DeserializeObject<PlayerData>(serializedData);
            _data = deserializedData ?? new PlayerData();
        }

        private void Save()
        {
            string json = JsonConvert.SerializeObject(_data);
            SaveInternal(json);
        }
    }

    [Serializable]
    internal class PlayerData
    {
        public List<LevelData> LevelDataList;

        public PlayerData()
        {
            LevelDataList = new List<LevelData>();
        }

        public PlayerData(List<LevelData> levelData)
        {
            LevelDataList = levelData;
        }
    }

    [Serializable]
    public class LevelData
    {
        public int Id;
        public LevelCompletionState Status;

        public LevelData(int id, LevelCompletionState state)
        {
            Id = id;
            Status = state;
        }
    }
}
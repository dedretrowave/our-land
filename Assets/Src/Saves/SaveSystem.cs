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
        
        private PlayerData _data = new();
        private string _pathToFile;

        public void SaveLevel(int id, LevelData data)
        {
            _data.LevelData[id] = data;
            
            Save();
        }

        public LevelData GetLevelById(int id)
        {
            return !_data.LevelData.ContainsKey(id) 
                ? new LevelData(LevelCompletionState.Incomplete) : _data.LevelData[id];
        }

        public void SaveMoney(int money)
        {
            _data.Money = money;

            Save();
        }

        public int GetMoney()
        {
            return _data.Money;
        }

        public SoundData GetSoundsSettings()
        {
            return _data.Sounds;
        }

        public void SaveSoundsSettings(SoundData data)
        {
            _data.Sounds = data;
            
            Save();
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            
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
        public Dictionary<int, LevelData> LevelData = new();
        public SoundData Sounds;
        public int Money;

        public PlayerData()
        {
            LevelData = new();
            Money = 0;
        }
    }

    [Serializable]
    public class SoundData
    {
        public bool IsMusicEnabled;

        public SoundData() { }
    }

    [Serializable]
    public class LevelData
    {
        public LevelCompletionState Status;

        public LevelData(LevelCompletionState state)
        {
            Status = state;
        }
    }
}
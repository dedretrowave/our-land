using System;
using System.Collections.Generic;
using Src.DI;
using Src.Map.Fraction;
using UnityEngine;

namespace Src.Saves
{
    public class PlayerDataSaveSystem : MonoBehaviour
    {
        [SerializeField] private string _localPathToFile = "player.dat";
        
        private PlayerData _data = new();
        private SaveFileHandler _handler;

        public void SaveLevel(int id, LevelData data)
        {
            _data.LevelData[id] = data;
            
            _handler.Save(_data);
        }

        public LevelData GetLevelById(int id)
        {
            return !_data.LevelData.ContainsKey(id) 
                ? null : _data.LevelData[id];
        }

        public void SaveMoney(int money)
        {
            _data.Money = money;

            _handler.Save(_data);
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
            
            _handler.Save(_data);
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            DependencyContext.Dependencies.Add(typeof(PlayerDataSaveSystem), () => this);

            _handler = new SaveFileHandler(_localPathToFile);
            
            _data = _handler.Load<PlayerData>() ?? new PlayerData();
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
        public int OwnerId;

        public LevelData(Fraction owner)
        {
            OwnerId = owner.Id;
        }
    }
}
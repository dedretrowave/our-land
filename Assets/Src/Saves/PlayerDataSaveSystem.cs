using System;
using System.Collections.Generic;
using System.Linq;
using DI;
using Newtonsoft.Json;
using Src.Map.Fraction;
using UnityEngine;

namespace Src.Saves
{
    public class PlayerDataSaveSystem : MonoBehaviour
    {
        [SerializeField] private string _localPath = "player";
        [SerializeField] private SaveFileHandler _handler;
        
        private PlayerData _data = new();

        public void SaveLevel(LevelData data)
        {
            LevelData existingLevel = _data.LevelData.FirstOrDefault(level => level.Id == data.Id);

            if (existingLevel != null)
            {
                existingLevel.Id = data.Id;
                existingLevel.OwnerId = data.OwnerId;
            }
            else
            {
                _data.LevelData.Add(data);
            }
            
            _handler.Save(_localPath, _data);
        }

        public LevelData GetLevelById(int id)
        {
            return _data.LevelData.Find(item => item.Id == id);
        }

        public void SaveMoney(int money)
        {
            _data.Money = money;
            
            _handler.Save(_localPath, _data);
        }

        public int GetMoney()
        {
            return _data.Money;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            DependencyContext.Dependencies.Add(typeof(PlayerDataSaveSystem), () => this);

            _data = _handler.Load<PlayerData>(_localPath) ?? new PlayerData();
        }
    }

    [Serializable]
    public class PlayerData
    {
        public List<LevelData> LevelData = new();
        public int Money;

        public PlayerData()
        {
            LevelData = new();
            Money = 0;
        }
    }

    [Serializable]
    public class LevelData
    {
        public int Id;
        public int OwnerId;
        
        public LevelData() {}

        public LevelData(int id, int ownerId)
        {
            Id = id;
            OwnerId = ownerId;
        }

        public LevelData(LevelData data)
        {
            Id = data.Id;
            OwnerId = data.OwnerId;
        }
    }
}
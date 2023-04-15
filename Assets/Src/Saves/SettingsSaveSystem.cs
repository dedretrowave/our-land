using System;
using Src.DI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Src.Saves
{
    public class SettingsSaveSystem : MonoBehaviour
    {
        [SerializeField] private string _localPath = "settings";
        [SerializeField] private SaveFileHandler _handler;
        
        private SettingsData _data = new();

        public void SaveSoundsSettings(SoundData data)
        {
            _data.Sounds = data;
            _handler.Save(_localPath, _data);
        }
        
        public SoundData GetSoundsSettings()
        {
            return _data.Sounds;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);

            _data = _handler.Load<SettingsData>(_localPath);
            
            DependencyContext.Dependencies.Add(typeof(SettingsSaveSystem), () => this);
        }
    }

    [Serializable]
    public class SettingsData
    {
        public SoundData Sounds = new();
        
        public SettingsData() {}
    }
    
    [Serializable]
    public class SoundData
    {
        public bool IsMusicEnabled = true;

        public SoundData() { }
    }
}
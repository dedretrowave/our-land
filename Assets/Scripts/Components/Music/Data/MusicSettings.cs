using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Components.Music.Data
{
    [CreateAssetMenu(fileName = "MusicSettings", menuName = "Music", order = 1)]
    public class MusicSettings : ScriptableObject
    {
        [SerializeField] private TrackTypeDictionary _tracks;
        [SerializeField] private TrackType _trackByDefault;

        public AudioClip GetDefault()
        {
            return _tracks[_trackByDefault];
        }

        public AudioClip GetByType(TrackType type)
        {
            return _tracks[type];
        }
    }
    
    public enum TrackType
    {
        MainMenu,
        Map,
        Level,
        Success,
        Failure,
    }
    
    [Serializable]
    public class TrackTypeDictionary : SerializableDictionary<TrackType, AudioClip> {}
}
using System;
using System.Collections.Generic;
using Src.DI;
using Src.Levels.Level;
using Src.Saves;
using Src.SerializableDictionary.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Src.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private TrackToType _tracks;
        [SerializeField] private TrackType _trackTypeByDefault;

        [SerializeField] private UnityEvent<bool> OnIsMusicPlayingChanged;

        private AudioSource _currentlyPlaying;

        private PlayerDataSaveSystem _saves;
        private SoundData _data;
        
        public void EnableLevelTrack()
        {
            SetTrackByType(TrackType.Level);
        }

        public void EnableMapTrack()
        {
            SetTrackByType(TrackType.Map);
        }

        public void EnableLevelFinishedTrack(Level level)
        {
            if (level.IsControlledByPlayer)
            {
                EnableSuccessTrack();
            }
            else
            {
                EnableFailureTrack();
            }
        }

        private void EnableSuccessTrack()
        {
            SetTrackByType(TrackType.Success);
        }

        private void EnableFailureTrack()
        {
            SetTrackByType(TrackType.Failure);
        }

        public void SetMusicPlaying(bool isPlaying)
        {
            _data.IsMusicEnabled = isPlaying;
            foreach (var track in _tracks)
            {
                track.Value.mute = !isPlaying;
            }
            _saves.SaveSoundsSettings(_data);
            OnIsMusicPlayingChanged.Invoke(isPlaying);
        }

        private void SetTrackByType(TrackType type)
        {
            _currentlyPlaying.mute = true;
            _currentlyPlaying = _tracks[type];
            _currentlyPlaying.mute = false;
            _currentlyPlaying.Play();
        }
        
        private void Start()
        {
            _saves = DependencyContext.Dependencies.Get<PlayerDataSaveSystem>();
            _data = _saves.GetSoundsSettings() ?? new SoundData();

            _currentlyPlaying = _tracks[_trackTypeByDefault];
            _currentlyPlaying.Play();
            
            SetMusicPlaying(_data.IsMusicEnabled);
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
    internal class TrackToType : SerializableDictionary<TrackType, AudioSource> {}
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(TrackToType))]
    internal class TrackToTypeUI : SerializableDictionaryPropertyDrawer {}
#endif
}
using System;
using DI;
using Src.Saves;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private TrackToType _tracks;
        [SerializeField] private TrackType _trackTypeByDefault;

        [SerializeField] private UnityEvent<bool> OnIsMusicPlayingChanged;

        private AudioSource _currentlyPlaying;

        private SettingsSaveSystem _saves;
        private SoundData _data;
        
        public void EnableLevelTrack()
        {
            SetTrackByType(TrackType.Level);
        }

        public void EnableMapTrack()
        {
            SetTrackByType(TrackType.Map);
        }

        // public void EnableLevelFinishedTrack(Level level)
        // {
        //     if (level.IsControlledByPlayer)
        //     {
        //         EnableSuccessTrack();
        //     }
        //     else
        //     {
        //         EnableFailureTrack();
        //     }
        // }

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

            if (_data.IsMusicEnabled)
            {
                _currentlyPlaying.Play();
            }
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
            _saves = DependencyContext.Dependencies.Get<SettingsSaveSystem>();
            _data = _saves.GetSoundsSettings() ?? new SoundData();
            
            _currentlyPlaying = _tracks[_trackTypeByDefault];

            if (_data.IsMusicEnabled)
            {
                _currentlyPlaying.Play();
            }
            
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
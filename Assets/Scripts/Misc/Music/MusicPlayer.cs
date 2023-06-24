using System;
using DI;
using Misc.Music.Data;
using Src.Saves;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Misc.Music
{
    public class MusicPlayer : MonoBehaviour
    {
        // [SerializeField] private TrackTypeDictionary _tracks;
        // [SerializeField] private TrackType _trackTypeByDefault;

        private AudioSource _currentlyPlaying;

        private SettingsSaveSystem _saves;
        private SoundData _data;
        
        private void EnableLevelTrack()
        {
            SetTrackByType(TrackType.Level);
        }

        private void EnableMapTrack()
        {
            SetTrackByType(TrackType.Map);
        }

        private void EnableWinTrack()
        {
            SetTrackByType(TrackType.Success);
        }

        private void EnableLoseTrack()
        {
            SetTrackByType(TrackType.Failure);
        }

        public void SetMusicPlaying(bool isPlaying)
        {
            _data.IsMusicEnabled = isPlaying;
            // foreach (var track in _tracks)
            // {
            //     track.Value.mute = !isPlaying;
            // }
            _saves.SaveSoundsSettings(_data);

            if (_data.IsMusicEnabled)
            {
                _currentlyPlaying.Play();
            }
        }

        private void SetTrackByType(TrackType type)
        {
            _currentlyPlaying.mute = true;
            // _currentlyPlaying = _tracks[type];
            _currentlyPlaying.mute = false;
            _currentlyPlaying.Play();
        }
        
        private void Start()
        {
            _saves = DependencyContext.Dependencies.Get<SettingsSaveSystem>();
            _data = _saves.GetSoundsSettings() ?? new SoundData();
            
            // _currentlyPlaying = _tracks[_trackTypeByDefault];

            if (_data.IsMusicEnabled)
            {
                _currentlyPlaying.Play();
            }
            
            SetMusicPlaying(_data.IsMusicEnabled);
        }
    }
}
using Src.DI;
using Src.Saves;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _music;

        [SerializeField] private UnityEvent<bool> OnIsMusicPlayingChanged;

        private PlayerDataSaveSystem _saves;
        private SoundData _data;
        
        private void Start()
        {
            DontDestroyOnLoad(this);
            _saves = DependencyContext.Dependencies.Get<PlayerDataSaveSystem>();
            _data = _saves.GetSoundsSettings() ?? new SoundData();
            
            SetMusicPlaying(_data.IsMusicEnabled);
        }

        public void SetMusicPlaying(bool isPlaying)
        {
            _data.IsMusicEnabled = isPlaying;
            _music.mute = !isPlaying;
            _saves.SaveSoundsSettings(_data);
            OnIsMusicPlayingChanged.Invoke(isPlaying);
        }
    }
}
using Src.DI;
using Src.Saves;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _music;
        [SerializeField] private AudioSource _sounds;

        [SerializeField] private UnityEvent<bool> OnIsMusicPlayingChanged;

        private SaveSystem _saves;
        private SoundData _data;
        
        private void Start()
        {
            DontDestroyOnLoad(this);
            _saves = DependencyContext.Dependencies.Get<SaveSystem>();
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
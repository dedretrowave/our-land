using UnityEngine;

namespace Src.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _music;

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        public void SetMusicPlaying(bool isPlaying)
        {
            _music.mute = !isPlaying;
        }
    }
}
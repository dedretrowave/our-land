using UnityEngine;

namespace Components.Music.View
{
    public class MusicView : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;

        public void SetTrack(AudioClip track)
        {
            _source.Stop();
            _source.clip = track;
            _source.Play();
        }

        public void SetEnabled(bool value)
        {
            _source.mute = !value;
        }
    }
}
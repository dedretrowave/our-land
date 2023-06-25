using UnityEngine;

namespace Components
{
    public class SoundTrigger : MonoBehaviour
    {
        [SerializeField] private AudioSource _hurtSound;

        public void TriggerHurt()
        {
            _hurtSound.Play();
        }
    }
}
using UnityEngine;

namespace Src.Garrisons.FX
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
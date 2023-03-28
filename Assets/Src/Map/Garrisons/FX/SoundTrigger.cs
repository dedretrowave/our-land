using UnityEngine;

namespace Src.Map.Garrisons.FX
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
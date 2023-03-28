using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Src.Map.Garrisons.FX
{
    public class AnimationTrigger : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Animator _animator;

        [SerializeField] private float _minTimeout = 2f;
        [SerializeField] private float _maxTimeout = 5f;
        
        private static readonly int Trigger = Animator.StringToHash("Random");
        private static readonly int Hurt = Animator.StringToHash("Hurt");

        private Coroutine _routine;

        public void TriggerHurt()
        {
            _animator.SetTrigger(Hurt);
        }

        private void Start()
        {
            _routine = StartCoroutine(TriggerAfterTimeoutContinuously());
        }

        private void OnDestroy()
        {
            StopCoroutine(_routine);
        }

        private IEnumerator TriggerAfterTimeoutContinuously()
        {
            float timeout = Random.Range(_minTimeout, _maxTimeout);

            yield return new WaitForSeconds(timeout);
            
            _animator.SetTrigger(Trigger);

            yield return TriggerAfterTimeoutContinuously();
        }
    }
}
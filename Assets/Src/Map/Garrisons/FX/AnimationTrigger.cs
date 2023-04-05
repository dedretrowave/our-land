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
        
        private readonly int _trigger = Animator.StringToHash("Random");
        private readonly int _hurt = Animator.StringToHash("Hurt");
        private readonly int _win = Animator.StringToHash("Win");

        private Coroutine _routine;

        public void TriggerWin()
        {
            _animator.SetTrigger(_win);
        }

        public void TriggerHurt()
        {
            _animator.SetTrigger(_hurt);
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
            
            _animator.SetTrigger(_trigger);

            yield return TriggerAfterTimeoutContinuously();
        }
    }
}
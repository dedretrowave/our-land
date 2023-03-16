using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Src.Divisions.Garrison.UI
{
    public class RandomAnimationTrigger : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Animator _animator;

        [SerializeField] private float _minTimeout = 2f;
        [SerializeField] private float _maxTimeout = 5f;
        
        private static readonly int Trigger = Animator.StringToHash("Trigger");

        private Coroutine _routine;

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
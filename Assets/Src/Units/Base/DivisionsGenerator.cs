using System.Collections;
using UnityEngine;

namespace Src.Units.Base
{
    public class DivisionsGenerator : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Garrison _garrison;

        [Header("Parameters")]
        [SerializeField] private float _generationRate = 2f;
        [SerializeField] private float _generationFreezeTimeout = 5f;

        private Coroutine _generationRoutine;

        public void StartGeneration()
        {
            if (_generationRoutine != null) return;
            
            _generationRoutine = StartCoroutine(Generate());
        }

        public void StopForTimeout()
        {
            StartCoroutine(Freeze());
        }

        public void StopGeneration()
        {
            StopCoroutine(_generationRoutine);
            _generationRoutine = null;
        }

        private void Start()
        {
            StartGeneration();
        }

        private void Create()
        {
            _garrison.IncreaseByOne();
        }

        private IEnumerator Freeze()
        {
            StopGeneration();

            yield return new WaitForSeconds(_generationFreezeTimeout);
            
            StartGeneration();
        }

        private IEnumerator Generate()
        {
            yield return new WaitForSeconds(_generationRate);
            
            Create();

            yield return Generate();
        }
    }
}
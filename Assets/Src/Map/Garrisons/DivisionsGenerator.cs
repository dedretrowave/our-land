using System.Collections;
using UnityEngine;

namespace Src.Map.Garrisons
{
    public class DivisionsGenerator : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Garrison _garrison;
        
        [Header("Parameters")]
        [SerializeField] private float _pauseBetweenSpawnsInSeconds = 2f;
        [SerializeField] private float _generationFreezeTimeout = 5f;

        private Coroutine _generationRoutine;

        public void Init(int generationRate)
        {
            _pauseBetweenSpawnsInSeconds = generationRate;
        }

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
            if (_generationRoutine == null) return;
            
            StopCoroutine(_generationRoutine);
            _generationRoutine = null;
        }

        private void Start()
        {
            StartGeneration();
        }

        private IEnumerator Freeze()
        {
            StopGeneration();

            yield return new WaitForSeconds(_generationFreezeTimeout);
            
            StartGeneration();
        }

        private IEnumerator Generate()
        {
            yield return new WaitForSeconds(_pauseBetweenSpawnsInSeconds);
            
            Create();

            yield return Generate();
        }
        
        private void Create()
        {
            _garrison.Increase();
        }
    }
}
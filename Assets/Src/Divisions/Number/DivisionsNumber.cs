using System.Collections;
using UnityEngine;

namespace Src.Divisions.Number
{
    public class DivisionsNumber : MonoBehaviour
    {
        [SerializeField] private float _increaseTimeSpan;
        [SerializeField] private float _freezeTimeSpan;
        
        private int _number;
        private Coroutine _increaseRoutine;

        public void Increase(int amount = 1)
        {
            _number += amount;
        }

        public void Decrease(int amount = 1)
        {
            int newNumber = _number - amount;

            if (newNumber <= 0)
            {
                _number = 0;
                return;
            }
            
            _number = newNumber;
        }

        public void StartIncreasing()
        {
            _increaseRoutine = StartCoroutine(IncreaseContinuously());
        }
        
        public void FreezeIncreasing()
        {
            StopCoroutine(_increaseRoutine);

            StartCoroutine(IncreaseContinuouslyAfterCooldown());
        }

        private IEnumerator IncreaseContinuouslyAfterCooldown()
        {
            yield return new WaitForSeconds(_freezeTimeSpan);

            _increaseRoutine = StartCoroutine(IncreaseContinuously());
        }

        private IEnumerator IncreaseContinuously()
        {
            yield return new WaitForSeconds(_increaseTimeSpan);
            
            Increase();

            yield return IncreaseContinuously();
        }
    }
}
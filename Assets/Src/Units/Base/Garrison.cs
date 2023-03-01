using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Units.Base
{
    public class Garrison : MonoBehaviour
    {
        public int Amount => _amount;

        [Header("Parameters")]
        [SerializeField] private int _initialNumber;
        [SerializeField] private float _changeRateInSeconds = 0.02f;
        
        [Header("Events")]
        [SerializeField] private UnityEvent _onNumberEqualsZero;
        [SerializeField] private UnityEvent<int> _onNumberChange;

        private int _amount;
        
        public void IncreaseToNumber(int number)
        {
            StartCoroutine(ChangeToNumber(number, Increase));
        }

        public void DecreaseToNumber(int number)
        {
            StartCoroutine(ChangeToNumber(number, Decrease));
        }

        private void Increase()
        {
            SetNumber(_amount + 1);
        }

        private void Decrease()
        {
            SetNumber(_amount - 1);
        }

        private void Awake()
        {
            SetNumber(_initialNumber);
        }

        private IEnumerator ChangeToNumber(int number, Action func)
        {
            if (_amount == number) yield break;

            yield return new WaitForSeconds(_changeRateInSeconds);

            func();

            yield return ChangeToNumber(number, func);
        }

        private void SetNumber(int value)
        {
            if (value <= 0)
            {
                _amount = 0;
                _onNumberEqualsZero.Invoke();
                return;
            }
                
            _amount = value;
            _onNumberChange.Invoke(_amount);
        }
    }
}
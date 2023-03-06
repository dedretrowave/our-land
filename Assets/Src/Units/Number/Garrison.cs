using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Units.Number
{
    public class Garrison : MonoBehaviour
    {
        public int Amount => _amount;

        [Header("Parameters")]
        [SerializeField] private int _initialNumber;

        [Header("Events")]
        [SerializeField] private UnityEvent _onNumberEqualsZero;
        [SerializeField] private UnityEvent<int> _onNumberChange;

        private int _amount;

        public void DecreaseToZero()
        {
            SetNumber(0);
        }

        public void Increase()
        {
            SetNumber(_amount + 1);
        }

        public void Decrease()
        {
            SetNumber(_amount - 1);
        }

        private void Awake()
        {
            SetNumber(_initialNumber);
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
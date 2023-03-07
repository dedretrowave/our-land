using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Src.Divisions.Garrison
{
    public class Garrison : MonoBehaviour
    {
        public int Amount => _amount;

        [Header("Parameters")]
        [SerializeField] private int _initialNumber;
        
        [Header("Events")]
        [SerializeField] private UnityEvent _onNumberBelowZero;
        [SerializeField] private UnityEvent<int> _onNumberChange;

        private int _amount;

        public void Init(int number)
        {
            _initialNumber = number;
            SetNumber(_initialNumber);
        }

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

        private void SetNumber(int value)
        {
            if (value < 0)
            {
                _amount = 0;
                _onNumberBelowZero.Invoke();
                return;
            }
                
            _amount = value;
            _onNumberChange.Invoke(_amount);
        }
    }
}
using UnityEngine;
using UnityEngine.Events;

namespace Src.Garrisons
{
    public class Garrison : MonoBehaviour
    {
        public int Amount => _amount;

        [Header("Parameters")]
        [SerializeField] private int _initialNumber;
        
        [Header("Events")]
        [SerializeField] private UnityEvent _onNumberBelowZero;
        [SerializeField] private UnityEvent<int> _onNumberChange;
        [SerializeField] private UnityEvent _onDamageTaken;

        private int _amount;

        private void Start()
        {
            SetNumber(_initialNumber);
        }

        public void TakeDamage()
        {
            _onDamageTaken.Invoke();
            Decrease();
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
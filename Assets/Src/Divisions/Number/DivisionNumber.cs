using UnityEngine;
using UnityEngine.Events;

namespace Src.Divisions.Number
{
    public class DivisionNumber : MonoBehaviour
    {
        private int _number = 50;
        private Coroutine _increaseRoutine;

        public int Number => _number;

        [HideInInspector] public UnityEvent OnNumberEqualsZero;
        [HideInInspector] public UnityEvent<int> OnNumberChange;

        public void Increase(int amount = 1)
        {
            _number += amount;
            OnNumberChange.Invoke(_number);
        }

        public void Decrease(int amount = 1)
        {
            int newNumber = _number - amount;

            if (newNumber <= 0)
            {
                _number = 0;
                OnNumberChange.Invoke(_number);
                OnNumberEqualsZero.Invoke();
                return;
            }
            
            OnNumberChange.Invoke(_number);
            
            _number = newNumber;
        }
    }
}
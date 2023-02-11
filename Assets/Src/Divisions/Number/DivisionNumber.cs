using UnityEngine;
using UnityEngine.Events;

namespace Src.Divisions.Number
{
    public class DivisionNumber : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnNumberEqualsZero;
        
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
                OnNumberEqualsZero.Invoke();
                return;
            }
            
            _number = newNumber;
        }
    }
}
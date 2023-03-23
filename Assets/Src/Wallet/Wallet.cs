using System;
using Src.DI;
using Src.Saves;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Wallet
{
    public class Wallet : MonoBehaviour
    {
        private int _money;

        [SerializeField] private UnityEvent<int> _onMoneyChange;
        [SerializeField] private UnityEvent _onMoneyEqualsZero;

        public int Money
        {
            get => _money;
            private set
            {
                _money = value;
                _onMoneyChange.Invoke(_money);
                _save.SaveMoney(_money);
            } 
        }

        private SaveSystem _save;

        public void Increase(int amount = 1)
        {
            Money += amount;
        }

        public void Decrease(int amount = 1)
        {
            int decreasedAmount = _money - amount;

            if (decreasedAmount <= 0)
            {
                Money = 0;
                _onMoneyEqualsZero.Invoke();
                throw new Exception("Out of money");
            }

            Money = decreasedAmount;
            _onMoneyChange.Invoke(_money);
        }
        
        private void Start()
        {
            _save = DependencyContext.Dependencies.Get<SaveSystem>();

            Money = _save.GetMoney();
            
            DependencyContext.Dependencies.Add(typeof(Wallet), () => this);
        }
    }
}
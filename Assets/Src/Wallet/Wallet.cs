using System;
using DI;
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

        private PlayerDataSaveSystem _save;

        public void Increase(int amount = 1)
        {
            Money += amount;
        }

        public void Decrease(int amount = 1)
        {
            int decreasedAmount = _money - amount;

            Money = decreasedAmount switch
            {
                0 => 0,
                < 0 => throw new Exception("Not enough money"),
                _ => Money,
            };

            Money = decreasedAmount;
            _onMoneyChange.Invoke(_money);
        }
        
        private void Awake()
        {
            _save = DependencyContext.Dependencies.Get<PlayerDataSaveSystem>();

            _money = _save.GetMoney();
            _onMoneyChange.Invoke(_money);
            
            DependencyContext.Dependencies.Add(typeof(Wallet), () => this);
        }
    }
}
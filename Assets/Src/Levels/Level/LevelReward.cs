using System;
using DI;
using UnityEngine;

namespace Src.Levels.Level
{
    public class LevelReward : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private int _amount;

        private Wallet.Wallet _wallet;

        public int Amount => _amount;

        public void Apply(Level level)
        {
            if (level.IsControlledByPlayer)
            {
                _wallet.Increase(_amount);
            }
        }

        private void Start()
        {
            _wallet = DependencyContext.Dependencies.Get<Wallet.Wallet>();
        }
    }
}
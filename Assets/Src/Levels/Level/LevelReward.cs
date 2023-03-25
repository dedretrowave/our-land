using System;
using Src.DI;
using UnityEngine;

namespace Src.Levels.Level
{
    public class LevelReward : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private int _amount;

        private Wallet.Wallet _wallet;

        public void ApplyByLevelStatus(LevelCompletionState status)
        {
            if (status == LevelCompletionState.Complete)
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
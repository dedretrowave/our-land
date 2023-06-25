using System;

namespace Player.Wallet.Models
{
    public class WalletModel
    {
        private int _money;

        public int Money => _money;

        public WalletModel(int money = 0)
        {
            _money = money;
        }

        public void Set(int amount)
        {
            if (amount < 0) return;

            _money = amount;
        }

        public void Add(int amount)
        {
            _money += amount;
        }

        public void Reduce(int amount)
        {
            int reducedAmount = _money - amount;

            if (reducedAmount < 0)
            {
                throw new OutOfMoneyException();
            }

            _money = reducedAmount;
        }
    }
    
    public class OutOfMoneyException : Exception {}
}
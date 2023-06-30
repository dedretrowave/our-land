using DI;
using Player.Wallet.Models;

namespace Save
{
    public class WalletModelLoader
    {
        private const string LocalPath = "wallet";

        private SaveFileHandler _saveFileHandler;

        private WalletModel _wallet;

        public WalletModelLoader()
        {
            _saveFileHandler = DependencyContext.Dependencies.Get<SaveFileHandler>();

            _wallet = _saveFileHandler.Load<WalletModel>(LocalPath) ?? new();
        }

        public int GetMoney()
        {
            return _wallet.Money;
        }

        public void SaveMoney(int amount)
        {
            _wallet = new(amount);
            _saveFileHandler.Save(LocalPath, _wallet);
        }
    }
}
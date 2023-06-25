using Player.Wallet.Models;
using Player.Wallet.Views;
using Save;

namespace Player.Wallet.Presenters
{
    public class WalletPresenter
    {
        private WalletModelLoader _walletModelLoader;
        private WalletModel _model;
        
        private WalletView _view;

        public WalletPresenter(WalletView view)
        {
            _walletModelLoader = new();

            int savedMoney = _walletModelLoader.GetMoney();
            
            _model = new(savedMoney);
            _view = view;
            _view.SetMoney(_model.Money);
        }

        public void AddMoney(int amount)
        {
            _model.Add(amount);
            _view.SetMoney(_model.Money);
            SaveMoney();
        }

        public void ReduceMoney(int amount)
        {
            try
            {
                _model.Reduce(amount);
            }
            catch (OutOfMoneyException)
            {
                // ignored
            }
            
            _view.SetMoney(_model.Money);
            SaveMoney();
        }
        
        private void SaveMoney()
        {
            _walletModelLoader.SaveMoney(_model.Money);
        }

        public void Hide()
        {
            _view.Hide();
        }

        public void Show()
        {
            _view.Show();
        }
    }
}
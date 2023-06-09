using Player.Wallet.Models;
using Player.Wallet.Views;

namespace Player.Wallet.Presenters
{
    public class WalletPresenter
    {
        private WalletModel _model;
        
        private WalletView _view;

        public WalletPresenter(WalletView view)
        {
            _model = new();
            _view = view;
        }

        public void AddMoney(int amount)
        {
            _model.Add(amount);
            _view.SetMoney(_model.Money);
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
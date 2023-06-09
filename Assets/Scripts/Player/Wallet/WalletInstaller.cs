using DI;
using Player.Wallet.Presenters;
using Player.Wallet.Views;
using UnityEngine;

namespace Player.Wallet
{
    public class WalletInstaller : MonoBehaviour
    {
        private WalletPresenter _presenter;

        private WalletView _view;

        public void Construct()
        {
            _view = GetComponentInChildren<WalletView>();
            
            _presenter = new(_view);
        }

        public void DisplayReward(int amount)
        {
            _presenter.AddMoney(amount);
        }

        public void Hide()
        {
            _presenter.Hide();
        }

        public void Show()
        {
            _presenter.Show();
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new(typeof(WalletInstaller), () => this));
        }
    }
}
using DI;
using EventBus;
using Player.Wallet.Presenters;
using Player.Wallet.Views;
using UnityEngine;

namespace Player.Wallet
{
    public class WalletInstaller : MonoBehaviour
    {
        private WalletPresenter _presenter;

        private WalletView _view;

        private EventBus.EventBus _eventBus;

        public void Construct()
        {
            _eventBus = EventBus.EventBus.Instance;

            _view = GetComponentInChildren<WalletView>();
            
            _presenter = new(_view);
            
            _eventBus.AddListener<int>(EventName.ON_SKIN_IN_SHOP_PURCHASED, _presenter.ReduceMoney);
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
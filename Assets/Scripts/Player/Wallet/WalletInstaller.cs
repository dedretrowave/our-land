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
            _eventBus.AddListener(EventName.ON_LEVEL_STARTED, _presenter.Hide);
            _eventBus.AddListener(EventName.ON_LEVEL_ENDED, _presenter.Show);
        }

        public void DisplayReward(int amount)
        {
            _presenter.AddMoney(amount);
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new(typeof(WalletInstaller), () => this));
        }
    }
}
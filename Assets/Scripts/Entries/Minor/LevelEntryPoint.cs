using DI;
using EventBus;
using Level;
using Level.Models;
using Map;
using Map.UI.Presenters;
using Map.UI.Views;
using Player.Wallet;
using Region.Presenters;
using UnityEngine;

namespace Entries
{
    public class LevelEntryPoint : MonoBehaviour
    {
        private EventBus.EventBus _eventBus;
        
        private LevelInstaller _levelInstaller;
        private MapRegionInstaller _mapRegionInstaller;
        private WalletInstaller _walletInstaller;
        
        private LevelFinishedView _levelFinishedView;

        private LevelFinishedPresenter _levelFinishedPresenter;
        private RegionPresenter _regionPresenter;

        private LevelConfig _levelConfig;

        private int _reward = 0;
        
        private void Start()
        {
            _levelFinishedView = DependencyContext.Dependencies.Get<LevelFinishedView>();
        }

        private void Awake()
        {
            _eventBus = EventBus.EventBus.Instance;
            DependencyContext.Dependencies.Add(new(typeof(LevelEntryPoint), () => this));
        }
        
        public void Init(LevelConfig levelPrefab, MapRegionInstaller mapRegion)
        {
            _levelConfig = Instantiate(levelPrefab, transform);
            _levelInstaller = _levelConfig.GetComponent<LevelInstaller>();
            _walletInstaller = DependencyContext.Dependencies.Get<WalletInstaller>();
            _mapRegionInstaller = mapRegion;

            _levelConfig.Init(_mapRegionInstaller.CurrentOwner);

            LevelModel levelModel = new(_levelConfig.Characters, _levelConfig.Reward);

            _levelFinishedPresenter = new(_levelFinishedView);

            _levelInstaller.Construct(_levelConfig, levelModel);

            _levelFinishedView.OnDoubleRewardCalled += OnDoubleReward;

            _levelInstaller.OnWinWithReward += OnReward;

            _levelInstaller.OnWin += _mapRegionInstaller.SetPlayerOwner;

            _levelInstaller.OnLose += _levelFinishedPresenter.TriggerLose;
            
            _levelInstaller.OnEnd += Unsubscribe;
        }

        private void Unsubscribe()
        {
            if (_levelInstaller == null) return;

            _levelFinishedView.OnDoubleRewardCalled -= OnDoubleReward;

            _levelInstaller.OnWinWithReward -= OnReward;

            _levelInstaller.OnWin -= _mapRegionInstaller.SetPlayerOwner;

            _levelInstaller.OnLose -= _levelFinishedPresenter.TriggerLose;
            
            _levelInstaller.OnEnd -= Unsubscribe;
            
            _eventBus.RemoveListener(EventName.ON_REWARDED_WATCHED, ApplyDoubleReward);
            
            _levelConfig.Disable();
        }

        private void OnReward(int reward)
        {
            _reward = reward;
            
            _levelFinishedPresenter.DisplayReward(reward);
            _walletInstaller.ApplyReward(reward);

            _eventBus.AddListener(EventName.ON_REWARDED_WATCHED, ApplyDoubleReward);
        }

        private void ApplyDoubleReward()
        {
            _levelFinishedPresenter.DisplayReward(_reward * 2);
            _walletInstaller.ApplyReward(_reward);
        }

        private void OnDoubleReward()
        {
            _eventBus.TriggerEvent(EventName.ON_REWARDED_OPENED);
        }
    }
}
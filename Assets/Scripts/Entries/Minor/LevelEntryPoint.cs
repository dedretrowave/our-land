using DI;
using Level;
using Level.Models;
using Level.Presenters;
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
        private LevelInstaller _levelInstaller;
        private MapRegionInstaller _mapRegionInstaller;
        private WalletInstaller _walletInstaller;
        
        private LevelFinishedView _levelFinishedView;

        private LevelFinishedPresenter _levelFinishedPresenter;
        private RegionPresenter _regionPresenter;
        
        public void Init(LevelConfig levelPrefab, MapRegionInstaller mapRegion)
        {
            LevelConfig levelConfig = Instantiate(levelPrefab, transform);
            _levelInstaller = levelConfig.GetComponent<LevelInstaller>();
            _walletInstaller = DependencyContext.Dependencies.Get<WalletInstaller>();
            _mapRegionInstaller = mapRegion;

            levelConfig.Init(_mapRegionInstaller.CurrentOwner);

            LevelModel levelModel = new(levelConfig.Characters, levelConfig.Reward);

            _levelFinishedPresenter = new(_levelFinishedView);

            _levelInstaller.OnStarted += _walletInstaller.Hide;

            _levelInstaller.Construct(levelConfig, levelModel);

            _levelInstaller.OnWinWithReward += _levelFinishedPresenter.DisplayReward;
            _levelInstaller.OnWinWithReward += _walletInstaller.DisplayReward;
            
            _levelInstaller.OnWin += _mapRegionInstaller.SetPlayerOwner;

            _levelInstaller.OnLose += _levelFinishedPresenter.TriggerLose;
            
            _levelInstaller.OnEnd += Unsubscribe;
            _levelInstaller.OnEnd += _walletInstaller.Show;
        }

        private void Unsubscribe()
        {
            if (_levelInstaller == null) return;
            
            _levelInstaller.OnStarted -= _walletInstaller.Hide;
            
            _levelInstaller.OnWinWithReward -= _levelFinishedPresenter.DisplayReward;
            _levelInstaller.OnWinWithReward -= _walletInstaller.DisplayReward;
            
            _levelInstaller.OnWin -= _mapRegionInstaller.SetPlayerOwner;

            _levelInstaller.OnLose -= _levelFinishedPresenter.TriggerLose;
            
            _levelInstaller.OnEnd -= Unsubscribe;
            _levelInstaller.OnEnd -= _walletInstaller.Show;
        }

        private void Start()
        {
            _levelFinishedView = DependencyContext.Dependencies.Get<LevelFinishedView>();
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new(typeof(LevelEntryPoint), () => this));
        }
    }
}
using System;
using DI;
using Level;
using Level.Models;
using Map;
using Map.UI.Presenters;
using Map.UI.Views;
using Region.Models;
using Region.Presenters;
using UnityEngine;

namespace Entries
{
    public class LevelEntryPoint : MonoBehaviour
    {
        private LevelInstaller _levelInstaller;
        private MapRegionInstaller _mapRegionInstaller;
        
        private LevelFinishedView _levelFinishedView;

        private LevelFinishedPresenter _levelFinishedPresenter;
        private RegionPresenter _regionPresenter;
        
        public void Init(LevelConfig levelPrefab, MapRegionInstaller mapRegion)
        {
            LevelConfig levelConfig = Instantiate(levelPrefab, transform);
            _levelInstaller = levelConfig.GetComponent<LevelInstaller>();
            _mapRegionInstaller = mapRegion;

            levelConfig.Init(mapRegion.CurrentOwner);

            LevelModel levelModel = new(levelConfig.Characters, levelConfig.Reward);

            _levelFinishedPresenter = new(_levelFinishedView, levelModel);

            _levelInstaller.Construct(levelConfig, levelModel);
            
            _levelInstaller.OnEnd += _levelFinishedPresenter.TriggerByLevelEnd;
            _levelInstaller.OnEnd += _mapRegionInstaller.SetRegionOwnerByLevelStatus;
        }

        private void OnDisable()
        {
            _levelInstaller.OnEnd -= _levelFinishedPresenter.TriggerByLevelEnd;
            _levelInstaller.OnEnd -= _mapRegionInstaller.SetRegionOwnerByLevelStatus;
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
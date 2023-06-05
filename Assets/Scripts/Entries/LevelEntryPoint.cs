using System;
using DI;
using Level;
using Level.Models;
using Map.UI.Presenters;
using Map.UI.Views;
using Region.Models;
using UnityEngine;

namespace Entries
{
    public class LevelEntryPoint : MonoBehaviour
    {
        private LevelInstaller _levelInstaller;
        
        private LevelFinishedView _levelFinishedView;

        private LevelFinishedPresenter _levelFinishedPresenter;
        
        public void Init(LevelConfig levelPrefab, RegionModel regionModel)
        {
            LevelConfig levelConfig = Instantiate(levelPrefab, transform);
            _levelInstaller = levelConfig.GetComponent<LevelInstaller>();
            
            levelConfig.Init(regionModel.CurrentOwner);
            
            LevelModel levelModel = new(levelConfig.Characters, levelConfig.Reward);

            _levelFinishedPresenter = new(_levelFinishedView, levelModel);
            
            _levelInstaller.Construct(levelConfig, levelModel);

            _levelInstaller.OnEnd += _levelFinishedPresenter.TriggerByLevelEnd;
        }

        private void OnDisable()
        {
            _levelInstaller.OnEnd -= _levelFinishedPresenter.TriggerByLevelEnd;
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
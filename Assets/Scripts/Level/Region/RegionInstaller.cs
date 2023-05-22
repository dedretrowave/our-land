using System;
using Characters.Base;
using Level.Region.Models;
using Level.Region.Presenters;
using Level.Region.Views;
using UnityEngine;

namespace Level.Region
{
    [Serializable]
    public class RegionInstaller : MonoBehaviour
    {
        private RegionView _regionView;

        private RegionPresenter _regionPresenter;

        private RegionModel _regionModel;

        public event Action<RegionInstaller, Character, Character> OnOwnerChange; 

        public void Construct(Character character)
        {
            _regionView = GetComponentInChildren<RegionView>();

            _regionModel = new(
                _regionView.GarrisonInitialCount,
                _regionView.GarrisonIncreaseRate,
                _regionView.DivisionSpawnRate);

            _regionPresenter = new(character, _regionView, _regionModel);

            StartCoroutine(_regionPresenter.IncreaseContinuously());
            
            _regionView.OnDamageTaken += _regionPresenter.TakeDamage;
            _regionView.OnGarrisonRelease += _regionPresenter.TryTargetRegion;
            _regionPresenter.OnSuccessfulRegionTarget += ReleaseGarrison;
            _regionPresenter.OnOwnerChange += ChangeOwner;
        }

        private void ChangeOwner(Character oldOwner, Character newOwner)
        {
            OnOwnerChange?.Invoke(this, oldOwner, newOwner);
        }

        private void ReleaseGarrison(RegionView targetPoint)
        {
            StartCoroutine(_regionPresenter.ReleaseGarrison(targetPoint));
        }

        private void OnDisable()
        {
            _regionView.OnDamageTaken -= _regionPresenter.TakeDamage;
            _regionView.OnGarrisonRelease -= _regionPresenter.TryTargetRegion;
            _regionPresenter.OnSuccessfulRegionTarget -= ReleaseGarrison;
        }
    }
}
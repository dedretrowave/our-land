using System;
using Characters.Base;
using Level.Region.Models;
using Level.Region.Presenters;
using Level.Region.Views;
using Src.Controls;
using UnityEngine;

namespace Level.Region
{
    [Serializable]
    public class RegionInstaller : MonoBehaviour
    {
        private RegionView _regionView;

        private RegionPresenter _regionPresenter;

        private RegionModel _regionModel;

        public RegionView View => _regionView;
        public RegionModel Model => _regionModel;

        public void Construct(Character character)
        {
            _regionView = GetComponentInChildren<RegionView>();

            _regionModel = new(
                _regionView.GarrisonInitialCount,
                _regionView.GarrisonIncreaseRate,
                _regionView.DivisionSpawnRate);

            _regionPresenter = new(character, _regionView, _regionModel);

            if (_regionModel.CurrentOwner.AllowsDivisionGeneration)
            {
                StartCoroutine(_regionPresenter.IncreaseContinuously());
            }

            _regionView.OnDamageTaken += _regionPresenter.TakeDamage;
            _regionView.OnGarrisonRelease += _regionPresenter.TryTargetRegion;
            _regionPresenter.OnSuccessfulRegionTarget += ReleaseGarrison;
            _regionPresenter.OnOwnerChange += ChangeOwner;
            
            ChangeOwner(_regionModel.CurrentOwner, _regionModel.CurrentOwner);
        }

        private void ChangeOwner(Character oldOwner, Character newOwner)
        {
            if (newOwner.AllowsDivisionGeneration)
            {
                StartCoroutine(_regionPresenter.IncreaseContinuously());
            }
            
            if (newOwner.Fraction == Fraction.Fraction.Player)
            {
                _regionView.gameObject.AddComponent<DivisionDeployment>();
            }
            else
            {
                Destroy(_regionView.GetComponent<DivisionDeployment>());
            }
            
            _regionView.NotifyOwnerChange(oldOwner, newOwner);
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
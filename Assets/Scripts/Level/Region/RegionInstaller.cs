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
        private GarrisonView _garrisonView;
        private RegionView _regionView;

        private RegionPresenter _regionPresenter;

        private RegionModel _regionModel;

        public RegionView View => _regionView;
        public RegionModel Model => _regionModel;

        public void Construct(Character character)
        {
            _garrisonView = GetComponentInChildren<GarrisonView>();
            _regionView = GetComponentInChildren<RegionView>();

            _regionModel = new(
                _garrisonView.GarrisonInitialCount,
                _garrisonView.GarrisonIncreaseRate,
                _garrisonView.DivisionSpawnRate);

            _regionPresenter = new(character, _regionView, _garrisonView, _regionModel);

            if (_regionModel.CurrentOwner.AllowsDivisionGeneration)
            {
                StartCoroutine(_regionPresenter.IncreaseContinuously());
            }

            _garrisonView.OnDamageTaken += _regionPresenter.TakeDamage;
            _garrisonView.OnGarrisonRelease += _regionPresenter.TryTargetRegion;
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
                _garrisonView.gameObject.AddComponent<DivisionDeployment>();
            }
            else
            {
                Destroy(_garrisonView.GetComponent<DivisionDeployment>());
            }
            
            _regionView.NotifyOwnerChange(oldOwner, newOwner);
        }

        private void ReleaseGarrison(GarrisonView targetPoint)
        {
            StartCoroutine(_regionPresenter.ReleaseGarrison(targetPoint));
        }

        private void OnDisable()
        {
            _garrisonView.OnDamageTaken -= _regionPresenter.TakeDamage;
            _garrisonView.OnGarrisonRelease -= _regionPresenter.TryTargetRegion;
            _regionPresenter.OnSuccessfulRegionTarget -= ReleaseGarrison;
        }
    }
}
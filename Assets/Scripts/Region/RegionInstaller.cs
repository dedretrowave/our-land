using System;
using Characters.Base;
using Region.Models;
using Region.Presenters;
using Region.Views;
using Src.Controls;
using UnityEngine;

namespace Region
{
    [Serializable]
    public class RegionInstaller : MonoBehaviour
    {
        private GarrisonView _garrisonView;
        private RegionView _regionView;

        private RegionPresenter _regionPresenter;

        private RegionModel _regionModel;
        private GarrisonModel _garrisonModel;

        public RegionView View => _regionView;

        public void Construct(Character character)
        {
            _garrisonView = GetComponentInChildren<GarrisonView>();
            _regionView = GetComponentInChildren<RegionView>();

            _garrisonModel = new(
                _garrisonView.GarrisonInitialCount,
                _garrisonView.GarrisonIncreaseRate,
                _garrisonView.DivisionSpawnRate);
            _regionModel = new();

            _regionPresenter = new(character, _regionView, _garrisonView, _regionModel, _garrisonModel);

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
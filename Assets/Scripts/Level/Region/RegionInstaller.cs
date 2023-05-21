using System;
using System.Collections;
using Characters.Base;
using DI;
using Level.Region.Models;
using Level.Region.Presenters;
using Level.Region.Views;
using UnityEngine;

namespace Level.Region
{
    [Serializable]
    public class RegionInstaller : MonoBehaviour
    {
        private Fraction.Fraction _fraction;
        private RegionView _regionView;

        private RegionPresenter _regionPresenter;

        private RegionModel _regionModel;

        private Coroutine _garrisonIncreaseRoutine;
        private Coroutine _garrisonDecreaseRoutine;

        public void Construct(Character character)
        {
            _regionView = GetComponentInChildren<RegionView>();

            _regionModel = new(
                _regionView.GarrisonInitialCount,
                _regionView.GarrisonIncreaseRate,
                _regionView.DivisionSpawnRate);

            _regionPresenter = new(character, _regionView, _regionModel);

            _garrisonIncreaseRoutine = StartCoroutine(_regionPresenter.IncreaseContinuously());
            
            _regionView.OnDamageTaken += _regionPresenter.TakeDamage;
            _regionView.OnGarrisonRelease += _regionPresenter.TryTargetRegion;
            _regionPresenter.OnSuccessfulRegionTarget += ReleaseGarrison;
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
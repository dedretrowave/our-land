using System;
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

            _regionModel = new(_regionView.GarrisonInitialCount, _regionView.GarrisonIncreaseRate);

            _regionPresenter = new(character, _regionView, _regionModel);

            _garrisonIncreaseRoutine = StartCoroutine(_regionPresenter.IncreaseContinuously());

            _regionPresenter.OnGarrisonRelease += ResetGarrisonCount;
            _regionView.OnDamageTaken += _regionPresenter.TakeDamage;
            _regionView.OnGarrisonRelease += _regionPresenter.Release;
        }

        private void ResetGarrisonCount()
        {
            StopCoroutine(_garrisonIncreaseRoutine);

            StartCoroutine(_regionPresenter.DecreaseContinuously());

            _garrisonIncreaseRoutine = StartCoroutine(_regionPresenter.IncreaseContinuously());
        }

        private void OnDisable()
        {
            _regionPresenter.OnGarrisonRelease -= ResetGarrisonCount;
            _regionView.OnDamageTaken -= _regionPresenter.TakeDamage;
            _regionView.OnGarrisonRelease -= _regionPresenter.Release;
        }
    }
}
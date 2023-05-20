using Characters.Base;
using DI;
using Level.Region.Models;
using Level.Region.Presenters;
using Level.Region.Views;
using UnityEngine;

namespace Level.Region
{
    public class RegionInstaller : MonoBehaviour
    {
        private Fraction.Fraction _fraction;
        private RegionView _garrisonView;

        private RegionPresenter _garrisonPresenter;

        private RegionModel _garrisonModel;

        private Coroutine _garrisonIncreaseRoutine;
        private Coroutine _garrisonDecreaseRoutine;

        public void Construct(Character character)
        {
            _garrisonView = DependencyContext.Dependencies.Get<RegionView>();

            _garrisonModel = new(_garrisonView.GarrisonInitialCount, _garrisonView.GarrisonIncreaseRate);

            _garrisonPresenter = new(character, _garrisonView, _garrisonModel);

            _garrisonIncreaseRoutine = StartCoroutine(_garrisonPresenter.IncreaseContinuously());

            _garrisonPresenter.OnGarrisonRelease += ResetGarrisonCount;
            _garrisonView.OnDamageTaken += _garrisonPresenter.TakeDamage;
            _garrisonView.OnGarrisonRelease += _garrisonPresenter.Release;
        }

        private void ResetGarrisonCount()
        {
            StopCoroutine(_garrisonIncreaseRoutine);

            StartCoroutine(_garrisonPresenter.DecreaseContinuously());

            _garrisonIncreaseRoutine = StartCoroutine(_garrisonPresenter.IncreaseContinuously());
        }

        private void OnDisable()
        {
            _garrisonPresenter.OnGarrisonRelease -= ResetGarrisonCount;
            _garrisonView.OnDamageTaken -= _garrisonPresenter.TakeDamage;
            _garrisonView.OnGarrisonRelease -= _garrisonPresenter.Release;
        }
    }
}
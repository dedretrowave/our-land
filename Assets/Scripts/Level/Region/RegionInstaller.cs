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
        private GarrisonView _garrisonView;

        private GarrisonPresenter _garrisonPresenter;

        private GarrisonModel _garrisonModel;

        private Coroutine _garrisonIncreaseRoutine;
        private Coroutine _garrisonDecreaseRoutine;

        public void Construct(Fraction.Fraction fraction)
        {
            _fraction = fraction;
            _garrisonView = DependencyContext.Dependencies.Get<GarrisonView>();

            _garrisonModel = new(_garrisonView.GarrisonInitialCount, _garrisonView.GarrisonIncreaseRate);

            _garrisonPresenter = new(fraction, _garrisonView, _garrisonModel);

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
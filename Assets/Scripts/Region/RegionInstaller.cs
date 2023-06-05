using System;
using Characters.Model;
using Characters.View;
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
        private CharacterView _characterView;

        private RegionPresenter _regionPresenter;
        private GarrisonPresenter _garrisonPresenter;

        private RegionModel _regionModel;
        private GarrisonModel _garrisonModel;

        public RegionView View => _regionView;

        public void Construct(CharacterModel character)
        {
            _garrisonView = GetComponentInChildren<GarrisonView>();
            _regionView = GetComponentInChildren<RegionView>();
            _characterView = GetComponentInChildren<CharacterView>();

            _garrisonModel = new(
                _garrisonView.GarrisonInitialCount,
                _garrisonView.GarrisonIncreaseRate,
                _garrisonView.DivisionSpawnRate);
            _regionModel = new();

            _regionPresenter = new(
                character,
                _regionView,
                _regionModel,
                _characterView);
            _garrisonPresenter = new(
                _garrisonView,
                _garrisonModel,
                _regionModel
            );

            if (_regionModel.CurrentOwner.AllowsDivisionGeneration)
            {
                StartCoroutine(_garrisonPresenter.IncreaseContinuously());
            }

            _garrisonView.OnDamageTaken += _garrisonPresenter.TakeDamage; 
            _garrisonView.OnGarrisonRelease += _garrisonPresenter.TryTargetRegion;

            _garrisonPresenter.OnDamageTaken += _regionPresenter.TakeDamage;
            _garrisonPresenter.OnSuccessfulRegionTarget += ReleaseGarrison;
            _garrisonPresenter.OnOwnerChangePossible += _regionPresenter.ChangeOwner;
            
            _regionPresenter.OnOwnerChange += ChangeOwner;
            
            ChangeOwner(_regionModel.CurrentOwner, _regionModel.CurrentOwner);
        }
        
        private void OnDisable()
        {
            _garrisonView.OnDamageTaken -= _garrisonPresenter.TakeDamage; 
            _garrisonView.OnGarrisonRelease -= _garrisonPresenter.TryTargetRegion;

            _garrisonPresenter.OnDamageTaken -= _regionPresenter.TakeDamage;
            _garrisonPresenter.OnSuccessfulRegionTarget -= ReleaseGarrison;
            _garrisonPresenter.OnOwnerChangePossible -= _regionPresenter.ChangeOwner;
            
            _regionPresenter.OnOwnerChange -= ChangeOwner;
        }

        private void ChangeOwner(CharacterModel oldOwner, CharacterModel newOwner)
        {
            if (newOwner.AllowsDivisionGeneration)
            {
                StartCoroutine(_garrisonPresenter.IncreaseContinuously());
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
            StartCoroutine(_garrisonPresenter.ReleaseGarrison(targetPoint));
        }
    }
}
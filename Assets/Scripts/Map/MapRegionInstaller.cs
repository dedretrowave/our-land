using System;
using Characters;
using Characters.Model;
using Characters.Presenter;
using Characters.View;
using Components;
using DI;
using Map.Presenters;
using Region.Models;
using Region.Views;
using UnityEngine;

namespace Map
{
    public class MapRegionInstaller : MonoBehaviour
    {
        private RegionView _regionView;
        private CharacterView _characterView;

        private CharacterModel _characterModel;
        private RegionModel _regionModel;

        private MapRegionPresenter _mapRegionPresenter;
        private CharacterPresenter _characterPresenter;

        private LevelSelector _selector;
        private CharacterContainer _characterContainer;

        public CharacterModel CurrentOwner => _regionModel.CurrentOwner;

        public event Action<RegionModel> OnModelUpdated; 

        public void Construct(RegionModel model)
        {
            _characterContainer = DependencyContext.Dependencies.Get<CharacterContainer>();

            _regionView = GetComponentInChildren<RegionView>();
            _characterView = GetComponentInChildren<CharacterView>();
            
            _regionModel = model;
            _characterModel = model.CurrentOwner;

            _mapRegionPresenter = new(_regionView, _regionModel);
            _characterPresenter = new(_characterView, _characterModel);

            _mapRegionPresenter.OnOwnerChange += _characterPresenter.SetSkinByCharacter;
            _mapRegionPresenter.OnOwnerChange += ChangeOwner;
            
            if (TryGetComponent(out LevelSelector selector))
            {
                _selector = selector;
                _selector.Construct(this);
                _selector.enabled = _characterModel.Fraction == Fraction.Fraction.Enemy;
            }
        }
        
        private void OnDisable()
        {
            _mapRegionPresenter.OnOwnerChange -= _characterPresenter.SetSkinByCharacter;
            _mapRegionPresenter.OnOwnerChange -= ChangeOwner;
        }

        public void ChangeOwner(CharacterModel _)
        {
            OnModelUpdated?.Invoke(_regionModel);
        }

        public void SetPlayerOwner()
        {
            SetRegionOwner(_characterContainer.GetByFraction(Fraction.Fraction.Player));
        }

        private void SetRegionOwner(CharacterModel newOwner)
        {
            _mapRegionPresenter.SetOwner(newOwner);
            _selector.enabled = newOwner.Fraction == Fraction.Fraction.Enemy;
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new(typeof(MapRegionInstaller), () => this));
        }
    }
}
using System;
using Characters;
using Characters.Model;
using Characters.Presenter;
using Characters.Skins;
using Characters.View;
using Components;
using DI;
using EventBus;
using Map.Presenters;
using Region.Models;
using Region.Views;
using UnityEngine;

namespace Map
{
    public class MapRegionInstaller : MonoBehaviour
    {
        private CharacterView _characterView;
        private RegionView _regionView;

        private CharacterModel _characterModel;
        private RegionModel _regionModel;

        private CharacterPresenter _characterPresenter;
        private MapRegionPresenter _mapRegionPresenter;

        private EventBus.EventBus _eventBus;
        private LevelSelector _selector;
        private CharacterContainer _characterContainer;

        public CharacterModel CurrentOwner => _regionModel.CurrentOwner;

        public event Action<RegionModel> OnModelUpdated; 

        public void Construct(RegionModel model)
        {
            _eventBus = EventBus.EventBus.Instance;
            _characterContainer = DependencyContext.Dependencies.Get<CharacterContainer>();

            _regionView = GetComponentInChildren<RegionView>();
            _characterView = GetComponentInChildren<CharacterView>();
            
            _regionModel = model;
            _characterModel = model.CurrentOwner;

            _mapRegionPresenter = new(_regionView, _regionModel);
            _characterPresenter = new(_characterView, _characterModel);

            _mapRegionPresenter.OnOwnerChange += _characterPresenter.SetSkinByCharacter;
            _mapRegionPresenter.OnOwnerChange += ChangeOwner;

            if (_characterPresenter.IsPlayer)
            {
                _eventBus.AddListener<Skin>(EventName.ON_CHARACTER_SKIN_CHANGE, _characterPresenter.SetSkin);
            }

            if (TryGetComponent(out LevelSelector selector))
            {
                _selector = selector;
                _selector.Construct(this);
                _selector.enabled = _characterModel.Fraction == Fraction.Fraction.Enemy;
            }
        }

        public void OnDisable()
        {
            if (_characterPresenter != null && _characterPresenter.IsPlayer)
            {
                _eventBus.RemoveListener<Skin>(EventName.ON_CHARACTER_SKIN_CHANGE, _characterPresenter.SetSkin);
            }

            try
            {
                _mapRegionPresenter.OnOwnerChange -= _characterPresenter.SetSkinByCharacter;
                _mapRegionPresenter.OnOwnerChange -= ChangeOwner;
            }
            catch (Exception)
            {
                //
            }
        }

        public void ChangeOwner(CharacterModel _)
        {
            OnModelUpdated?.Invoke(_regionModel);
        }

        public void SetPlayerOwner()
        {
            SetRegionOwner(_characterContainer.GetByFraction(Fraction.Fraction.Player));
            
            _eventBus.AddListener<Skin>(EventName.ON_CHARACTER_SKIN_CHANGE, _characterPresenter.SetSkin);
        }

        private void SetRegionOwner(CharacterModel newOwner)
        {
            _mapRegionPresenter.SetOwner(newOwner);
            _selector.enabled = newOwner.Fraction == Fraction.Fraction.Enemy;
            
            _eventBus.RemoveListener<Skin>(EventName.ON_CHARACTER_SKIN_CHANGE, _characterPresenter.SetSkin);
        }

        public void SetRandomEnemyOwner()
        {
            CharacterModel enemy = _characterContainer.GetRandomEnemy();
            
            SetRegionOwner(enemy);
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new(typeof(MapRegionInstaller), () => this));
        }
    }
}
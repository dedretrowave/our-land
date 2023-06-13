using System;
using Characters;
using Characters.Skins;
using Characters.Skins.SO;
using DI;
using EventBus;
using SkinShop.Models;
using SkinShop.Presenters;
using SkinShop.Views;
using UnityEditor;
using UnityEngine;

namespace SkinShop
{
    public class SkinShopInstaller : MonoBehaviour
    {
        [SerializeField] private SkinItemTypeCollectionDictionary _skinItems;

        private EventBus.EventBus _eventBus;

        private SkinShopPresenter _presenter;

        private SkinShopView _view;

        private void Awake()
        {
            CharacterContainer characterContainer = DependencyContext.Dependencies.Get<CharacterContainer>();

            Skin playerInitialSkin = characterContainer.GetByFraction(Fraction.Fraction.Player).Skin;

            _eventBus = EventBus.EventBus.Instance;
            
            foreach ((SkinItemType type, SkinItemCollection collection) in _skinItems)
            {
                collection.Construct(playerInitialSkin.GetItemByType(type));
            }
            
            _view = GetComponentInChildren<SkinShopView>();

            _presenter = new(_view, playerInitialSkin, _skinItems);

            _view.OnSelectedNext += _presenter.SelectNext;
            _view.OnSelectedPrev += _presenter.SelectPrev;
            _view.OnPurchased += _presenter.Purchase;
            _view.OnSelected += _presenter.Select;

            _presenter.OnSkinSelected += OnSkinSelect;
        }

        private void OnDisable()
        {
            if (_presenter == null || _view == null) return;
            
            _view.OnSelectedNext -= _presenter.SelectNext;
            _view.OnSelectedPrev -= _presenter.SelectPrev;
            _view.OnPurchased -= _presenter.Purchase;
            _view.OnSelected -= _presenter.Select;
            
            _presenter.OnSkinSelected -= OnSkinSelect;
        }

        private void OnSkinSelect(Skin selectedSkin)
        {
            _eventBus.TriggerEvent(EventName.ON_SKIN_IN_SHOP_SELECTED, selectedSkin);
        }
    }
    
    [Serializable]
    internal class SkinItemTypeCollectionDictionary : SerializableDictionary<SkinItemType, SkinItemCollection> {}
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SkinItemTypeCollectionDictionary))]
    internal class SkinItemTypeCollectionDictionaryUI : SerializableDictionaryPropertyDrawer {}
#endif
}
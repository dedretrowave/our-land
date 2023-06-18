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
        [SerializeField] private SkinItemTypeShopCollectionDictionary _skinItems;

        private EventBus.EventBus _eventBus;

        private SkinShopPresenter _presenter;

        private SkinShopView _view;

        public void Construct(Skin playerInitialSkin)
        {
            _eventBus = EventBus.EventBus.Instance;
            
            foreach ((SkinItemType type, SkinShopItemCollection collection) in _skinItems)
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
            
            _eventBus.AddListener(EventName.ON_LEVEL_STARTED, _presenter.HideButton);
            _eventBus.AddListener(EventName.ON_LEVEL_ENDED, _presenter.ShowButton);
        }

        public void Disable()
        {
            _view.OnSelectedNext -= _presenter.SelectNext;
            _view.OnSelectedPrev -= _presenter.SelectPrev;
            _view.OnPurchased -= _presenter.Purchase;
            _view.OnSelected -= _presenter.Select;
            
            _presenter.OnSkinSelected -= OnSkinSelect;
            
            _eventBus.RemoveListener(EventName.ON_LEVEL_STARTED, _presenter.HideButton);
            _eventBus.RemoveListener(EventName.ON_LEVEL_ENDED, _presenter.ShowButton);
        }

        private void OnSkinSelect(Skin selectedSkin)
        {
            _eventBus.TriggerEvent(EventName.ON_SKIN_IN_SHOP_SELECTED, selectedSkin);
        }
    }
    
    [Serializable]
    internal class SkinItemTypeShopCollectionDictionary : SerializableDictionary<SkinItemType, SkinShopItemCollection> {}
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SkinItemTypeShopCollectionDictionary))]
    internal class SkinItemTypeCollectionDictionaryUI : SerializableDictionaryPropertyDrawer {}
#endif
}
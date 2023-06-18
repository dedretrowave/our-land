using System;
using System.Collections.Generic;
using Characters.Skins;
using Characters.Skins.SO;
using EventBus;
using SkinShop.Models;
using SkinShop.Views;
using UnityEngine;

namespace SkinShop.Presenters
{
    public class SkinShopPresenter
    {
        private Dictionary<SkinItemType, SkinShopItemCollection> _skinItems;

        private SkinShopModel _model;
        
        private SkinShopView _view;

        private EventBus.EventBus _eventBus;
        
        public event Action<Skin> OnSkinSelected;

        public SkinShopPresenter(SkinShopView view, Skin initialSkin, Dictionary
            <SkinItemType, SkinShopItemCollection> items)
        {
            _eventBus = EventBus.EventBus.Instance;

            _model = new();
            _view = view;
            
            _model.SetSkin(initialSkin);
            _skinItems = new(items);
            
            _view.SetSkin(_model.Skin);
        }

        public void SelectNext(SkinItemType type)
        {
            SkinItem item = _skinItems[type].GetNextAndMove();

            SetItem(item);
        }

        public void SelectPrev(SkinItemType type)
        {
            SkinItem item = _skinItems[type].GetPrevAndMove();
            
            SetItem(item);
        }

        public void Purchase()
        {
            int cost = _model.Skin.GetTotalCost();
            
            foreach ((SkinItemType type, SkinShopItemCollection collection) in _skinItems)
            {
                SkinItem item = collection.GetById(_model.Skin.GetItemByType(type).Id);
                item.SetPurchased();
                _eventBus.TriggerEvent(EventName.ON_SKIN_IN_SHOP_PURCHASED, item);
            }

            _eventBus.TriggerEvent(EventName.ON_SKIN_IN_SHOP_PURCHASED, cost);
            _view.SetPrice(0);
        }

        public void Select()
        {
            Skin selectedSkin = _model.Skin;
            
            OnSkinSelected?.Invoke(selectedSkin);
        }

        private void SetItem(SkinItem item)
        {
            _model.SetSkinItem(item);
            _view.SetSkin(_model.Skin);
            _view.SetPrice(_model.Skin.GetTotalCost());
        }
    }
}
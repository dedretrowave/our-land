using System;
using System.Collections.Generic;
using Characters.Skins;
using Characters.Skins.SO;
using SkinShop.Models;
using SkinShop.Views;

namespace SkinShop.Presenters
{
    public class SkinShopPresenter
    {
        private Dictionary<SkinItemType, SkinItemCollection> _skinItems;

        private SkinShopModel _model;
        
        private SkinShopView _view;

        public event Action<int> OnItemsPurchased;
        public event Action<Skin> OnSkinSelected; 

        public SkinShopPresenter(SkinShopView view, Skin initialSkin, Dictionary<SkinItemType, SkinItemCollection> items)
        {
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
            
            OnItemsPurchased?.Invoke(cost);
        }

        public void Select()
        {
            Skin selectedSkin = _model.Skin;
            
            OnSkinSelected?.Invoke(selectedSkin);
        }

        private void SetItem(SkinItem item)
        {
            _model.SetSkinItem(item);
        }
    }
}
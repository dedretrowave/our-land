using System;
using Characters.Skins;
using Characters.Skins.SO;
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

        private SkinShopPresenter _presenter;

        private SkinShopView _view;

        public void Construct(Skin playerInitialSkin)
        {
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
        }

        private void OnDisable()
        {
            _view.OnSelectedNext -= _presenter.SelectNext;
            _view.OnSelectedPrev -= _presenter.SelectPrev;
            _view.OnPurchased -= _presenter.Purchase;
            _view.OnSelected -= _presenter.Select;
        }
    }
    
    [Serializable]
    internal class SkinItemTypeCollectionDictionary : SerializableDictionary<SkinItemType, SkinItemCollection> {}
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SkinItemTypeCollectionDictionary))]
    internal class SkinItemTypeCollectionDictionaryUI : SerializableDictionaryPropertyDrawer {}
#endif
}
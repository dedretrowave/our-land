using System;
using Src.DI;
using Src.Saves;
using Src.SkinShop.Items;
using Src.SkinShop.Items.Base;
using Src.SkinShop.Skin;
using UnityEngine;
using UnityEngine.Events;

namespace Src.SkinShop
{
    public class SkinShop : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Wallet.Wallet _wallet; 
        [SerializeField] private FractionSkin _playerSkin;

        [Header("Events")]
        [SerializeField] private UnityEvent<int> _onTotalPriceChange; 
        [SerializeField] private UnityEvent<Items.Skin> _onSkinChanged;
        [SerializeField] private UnityEvent _onItemPurchased;

        private Items.Skin _purchasableSkin;
        private SkinSaveSystem _skinSaveSystem;
        private int _totalPrice;

        public void SetSkinItem(SkinItem item)
        {
            _purchasableSkin.SetItem(item);
            _onSkinChanged.Invoke(_purchasableSkin);
            RecalculatePrice();
        }

        public void PurchaseItems()
        {
            _wallet.Decrease(_totalPrice);
            _totalPrice = 0;
            
            _purchasableSkin.Items.ForEach(item =>
            {
                item.IsPurchased = true;
                _skinSaveSystem.SaveItemPurchase(item);
            });
            _onItemPurchased.Invoke();
        }

        public void SelectSkin()
        {
            _playerSkin.SetSkin(_purchasableSkin);
        }

        private void OnEnable()
        {
            _skinSaveSystem = DependencyContext.Dependencies.Get<SkinSaveSystem>();

            Items.Skin playerSkin = new(_playerSkin.Skin);

            _purchasableSkin = new(playerSkin);

            foreach (SkinItemType type in Enum.GetValues(typeof(SkinItemType)))
            {
                SetSkinItem(_purchasableSkin.GetItemByType(type));
            }
        }

        private void RecalculatePrice()
        {
            _totalPrice = _purchasableSkin.GetTotalCost();
            _onTotalPriceChange.Invoke(_totalPrice);
        }
    }
}
using System;
using System.Collections.Generic;
using Src.SkinShop.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Src.SkinShop
{
    public class SkinShop : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Wallet.Wallet _wallet;
        [SerializeField] private PlayerSkinChanger _skinChanger;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<int> _onTotalPriceChange;
        [SerializeField] private UnityEvent _onItemPurchased;

        private Dictionary<SkinShopItemCategory, SkinShopItem> _itemsToCategory = new();

        private int _totalPrice;

        public void SetItem(SkinShopItem item)
        {
            _itemsToCategory[item.Category] = item;

            RecalculatePrice();
        }

        public void PurchaseItems()
        {
            foreach (var item in _itemsToCategory)
            {
                try
                {
                    _wallet.Decrease(item.Value.Price);
                    
                    item.Value.IsPurchased = true;
                    
                    _onItemPurchased.Invoke();
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
        }

        public void SelectSkin()
        {
            foreach (var item in _itemsToCategory)
            {
                switch (item.Value.Category)
                {
                    case SkinShopItemCategory.Flag:
                        _skinChanger.SetFlag(item.Value.Sprite);
                        break;
                    case SkinShopItemCategory.Eyes:
                        _skinChanger.SetEyes(item.Value.Sprite);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void RecalculatePrice()
        {
            _totalPrice = 0;

            foreach (var skinShopItem in _itemsToCategory)
            {
                if (skinShopItem.Value.IsPurchased)
                {
                    AddToTotalPrice(0);
                    continue;
                }
                
                AddToTotalPrice(skinShopItem.Value.Price);
            }
        }

        private void AddToTotalPrice(int amount)
        {
            _totalPrice += amount;
            _onTotalPriceChange.Invoke(_totalPrice);
        }
    }
}
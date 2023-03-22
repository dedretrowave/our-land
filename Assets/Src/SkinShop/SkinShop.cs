using System.Collections.Generic;
using System.Linq;
using Src.SkinShop.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Src.SkinShop
{
    public class SkinShop : MonoBehaviour
    {
        [SerializeField] private UnityEvent<int> _onTotalPriceChange;

        private Dictionary<SkinShopItemCategory, SkinShopItem> _itemsToCategory = new();

        private int _totalPrice;

        public void AddItem(SkinShopItem item)
        {
            if (!_itemsToCategory.ContainsKey(item.Category))
            {
                _itemsToCategory[item.Category] = item;
            }

            RecalculatePrice();
        }

        private void RecalculatePrice()
        {
            _totalPrice = 0;

            foreach (var skinShopItem 
                     in _itemsToCategory.Where(skinShopItem => !skinShopItem.Value.IsPurchased))
            {
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
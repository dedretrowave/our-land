using System;
using System.Collections.Generic;
using Src.SkinShop.Items.Base;
using UnityEngine;

namespace Src.SkinShop.Items
{
    [Serializable]
    public class Skin
    {
        [SerializeField] private List<SkinItem> _items;

        public List<SkinItem> Items => _items;

        public SkinItem GetItemByType(SkinItemType type)
        {
            return _items.Find(item => item.Type == type);
        }

        public void MarkItemsAsPurchased()
        {
            _items.ForEach(item => item.IsPurchased = true);
        }

        public void SetItem(SkinItem item)
        {
            int indexOfSkin = 
                _items.FindIndex(skinItems => skinItems.Type == item.Type);

            if (indexOfSkin == -1)
            {
                _items.Add(item);
            }
            else
            {
                _items[indexOfSkin] = item;
            }
        }

        public int GetTotalCost()
        {
            int totalCost = 0;
            
            _items.ForEach(item =>
            {
                if (!item.IsPurchased)
                {
                    totalCost += item.Price;
                }
            });

            return totalCost;
        }

        public Skin(Skin skin)
        {
            _items = new(skin.Items);
        }

        public Skin()
        {
            _items = new();
        }
    }
}
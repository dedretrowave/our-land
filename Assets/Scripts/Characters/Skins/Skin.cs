using System;
using System.Collections.Generic;
using Characters.Skins.SO;
using UnityEngine;

namespace Characters.Skins
{
    [Serializable]
    public class Skin
    {
        private List<SkinItem> _items = new();
        
        public Skin(List<SkinItemSO> data)
        {
            data.ForEach(item => _items.Add(new(item)));
        }

        public Skin(List<SkinItem> items)
        {
            items.ForEach(item => _items.Add(item));
        }

        public Skin() { }

        public SkinItem GetItemByType(SkinItemType type)
        {
            return _items.Find(item => item.Type == type);
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

        public void MarkItemsAsPurchased()
        {
            //
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

        public void IterateThroughItems(Action<SkinItem> callback)
        {
            _items.ForEach(callback);
        }
    }
}
using System;
using System.Collections.Generic;

namespace Characters.Skins
{
    [Serializable]
    public class Skin
    {
        private List<SkinItem> _items;

        public SkinItem GetItemByType(SkinItemType type)
        {
            return _items.Find(item => item.Type == type);
        }

        public void MarkItemsAsPurchased()
        {
            
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

        public Skin()
        {
            _items = new();
        }
    }
}
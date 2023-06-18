using System;
using System.Collections.Generic;
using Characters.Skins.SO;
using UnityEngine;

namespace Characters.Skins
{
    [Serializable]
    public class SkinItemCollection
    {
        [SerializeField] private List<SkinItemSO> _itemsSO = new();

        private List<SkinItem> _items = new();

        public List<SkinItem> Items => new(_items);

        public void Construct(List<SkinItemData> loadedItems = null)
        {
            if (loadedItems != null)
            {
                loadedItems.ForEach(item =>
                {
                    SkinItemSO so = _itemsSO.Find(so => so.Id == item.Id);

                    if (so == null) return;

                    _items.Add(new(
                        so.Type,
                        so.Id,
                        so.Sprite,
                        so.Price,
                        item.IsPurchased
                        ));
                });
                
                return;
            }
            
            _itemsSO.ForEach(item => _items.Add(new(item)));
        }

        public void SetItemPurchased(SkinItem item)
        {
            _items.Find(skinItem => skinItem.Id == item.Id).SetPurchased();
        }

        public SkinItem GetById(int id)
        {
            return _items.Find(item => item.Id == id);
        }
    }
}
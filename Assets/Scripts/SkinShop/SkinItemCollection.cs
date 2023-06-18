using System;
using System.Collections.Generic;
using Characters.Skins;
using Characters.Skins.SO;
using UnityEngine;

namespace SkinShop
{
    [Serializable]
    public class SkinItemCollection
    {
        [SerializeField] private List<SkinItemSO> _itemsSO = new();

        private List<SkinItem> _items = new();

        private int _selectedItemIndex;

        public void Construct(SkinItem selectedSkinItem = null)
        {
            _itemsSO.ForEach(item => _items.Add(new(item)));
            
            if (selectedSkinItem == null)
            {
                _selectedItemIndex = 0;
                return;
            }

            SkinItem currentItem = _items.Find(item => item.Id == selectedSkinItem.Id);

            _selectedItemIndex = _items.IndexOf(currentItem);
        }

        public void SetItemPurchased(SkinItem item)
        {
            _items.Find(skinItem => skinItem.Id == item.Id).SetPurchased();
        }

        public SkinItem GetById(int id)
        {
            return _items.Find(item => item.Id == id);
        }

        public SkinItem GetNextAndMove()
        {
            int nextIndex = _selectedItemIndex + 1;

            if (nextIndex >= _itemsSO.Count)
            {
                nextIndex = 0;
            }

            _selectedItemIndex = nextIndex;

            return new(_itemsSO[_selectedItemIndex]);
        }

        public SkinItem GetPrevAndMove()
        {
            int nextIndex = _selectedItemIndex - 1;

            if (nextIndex < 0)
            {
                nextIndex = _itemsSO.Count - 1;
            }

            _selectedItemIndex = nextIndex;

            return new(_itemsSO[_selectedItemIndex]);
        }

        private void LoadData()
        {
            
        }

        private void SaveData()
        {
            
        }
    }
}
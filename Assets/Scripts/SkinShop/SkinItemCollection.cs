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
        [SerializeField] private List<SkinItemSO> _items = new();

        private int _selectedItemIndex;

        public void Construct(SkinItem selectedSkinItem = null)
        {
            if (selectedSkinItem == null)
            {
                _selectedItemIndex = 0;
                return;
            }
            
            SkinItemSO currentItem = _items.Find(item => item.Id == selectedSkinItem.Id);

            _selectedItemIndex = _items.IndexOf(currentItem);
        }

        public SkinItem GetById(int id)
        {
            SkinItemSO skinItemSO = _items.Find(item => item.Id == id);
            
            return new(skinItemSO);
        }

        public SkinItem GetNextAndMove()
        {
            int nextIndex = _selectedItemIndex + 1;

            if (nextIndex >= _items.Count)
            {
                nextIndex = 0;
            }

            _selectedItemIndex = nextIndex;

            return new(_items[_selectedItemIndex]);
        }

        public SkinItem GetPrevAndMove()
        {
            int nextIndex = _selectedItemIndex - 1;

            if (nextIndex < 0)
            {
                nextIndex = _items.Count - 1;
            }

            _selectedItemIndex = nextIndex;

            return new(_items[_selectedItemIndex]);
        }
    }
}
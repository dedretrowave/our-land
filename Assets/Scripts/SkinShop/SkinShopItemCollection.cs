using System;
using System.Collections.Generic;
using Characters.Skins;
using Characters.Skins.SO;
using DI;
using UnityEngine;

namespace SkinShop
{
    [Serializable]
    public class SkinShopItemCollection
    {
        [SerializeField] private List<SkinItemSO> _itemsSO = new();

        private SkinItemsContainer _skinItemsContainer;

        private List<int> _items = new();

        private int _selectedItemIndex;

        public void Construct(SkinItem selectedSkinItem = null)
        {
            _skinItemsContainer = DependencyContext.Dependencies.Get<SkinItemsContainer>();

            _itemsSO.ForEach(so => _items.Add(so.Id));

            if (selectedSkinItem == null)
            {
                _selectedItemIndex = 0;
                return;
            }

            SkinItem currentItem = _skinItemsContainer
                .GetByIdAndType(selectedSkinItem.Type, selectedSkinItem.Id);

            _selectedItemIndex = _items.IndexOf(currentItem.Id);
        }

        public SkinItem GetById(int id)
        {
            return GetFromContainer(so => so.Id == id);
        }
        
        public SkinItem GetNextAndMove()
        {
            int nextIndex = _selectedItemIndex + 1;

            if (nextIndex >= _items.Count)
            {
                nextIndex = 0;
            }

            _selectedItemIndex = nextIndex;

            return GetFromContainer(so => so.Id == _items[_selectedItemIndex]);
        }

        public SkinItem GetPrevAndMove()
        {
            int nextIndex = _selectedItemIndex - 1;

            if (nextIndex < 0)
            {
                nextIndex = _itemsSO.Count - 1;
            }

            _selectedItemIndex = nextIndex;

            return GetFromContainer(so => so.Id == _items[_selectedItemIndex]);
        }

        private SkinItem GetFromContainer(Predicate<SkinItemSO> findFunc)
        {
            SkinItemSO selectedSO = _itemsSO.Find(findFunc);
            SkinItem selectedItem = _skinItemsContainer.GetByIdAndType(selectedSO.Type, selectedSO.Id);

            return selectedItem;
        }
    }
}
using System.Collections.Generic;
using DI;
using Src.Saves;
using Src.SkinShop.Items.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Src.SkinShop
{
    public class SkinShopItemContainer : MonoBehaviour
    {
        [SerializeField] private SkinItemType _containedType;
        [SerializeField] private List<SkinItem> _items;

        [Header("Events")]
        [SerializeField] private UnityEvent<SkinItem> _onItemSelected;

        private int _index;

        public SkinItem GetSkinItemById(int id)
        {
            return _items.Find(item => item.Id == id);
        }

        public void SelectNext()
        {
            _index++;

            if (_index >= _items.Count) _index = 0;
            
            SelectItem(_items[_index]);
        }

        public void SelectPrevious()
        {
            _index--;

            if (_index < 0) _index = _items.Count - 1;
            
            SelectItem(_items[_index]);
        }

        private void Awake()
        {
            SkinSaveSystem skinSaveSystem = DependencyContext.Dependencies.Get<SkinSaveSystem>();
            List<PurchasedSkinItemData> savedItems = skinSaveSystem.GetItemsByType(_containedType);

            savedItems.ForEach(savedItem =>
            {
                int index = _items.FindIndex(item => item.Id == savedItem.Id);
                _items[index].IsPurchased = savedItem.IsPurchased;
            });
        }

        private void SelectItem(SkinItem item)
        {
            _onItemSelected.Invoke(item);
        }
    }
}
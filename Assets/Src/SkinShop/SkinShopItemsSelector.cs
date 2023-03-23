using System.Collections.Generic;
using Src.SkinShop.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Src.SkinShop
{
    public class SkinShopItemsSelector : MonoBehaviour
    {
        [SerializeField] private List<SkinShopItem> _items;

        [Header("Events")]
        [SerializeField] private UnityEvent<SkinShopItem> _onItemSelected;

        private SkinShopItem _selectedItem;

        private int _index;

        public void SelectNext()
        {
            _index++;

            if (_index >= _items.Count) _index = 0;
            
            SelectFlag(_items[_index]);
        }

        public void SelectPrevious()
        {
            _index--;

            if (_index < 0) _index = _items.Count - 1;
            
            SelectFlag(_items[_index]);
        }

        private void Start()
        {
            SelectFlag(_items[_index]);
        }

        private void SelectFlag(SkinShopItem flag)
        {
            _selectedItem = flag;
            _onItemSelected.Invoke(_selectedItem);
        }
    }
}
using System.Collections.Generic;
using Characters.Skins;
using Characters.Skins.SO;
using DI;
using Src.Saves;
using UnityEngine;
using UnityEngine.Events;

namespace Src.SkinShop
{
    public class SkinShopItemContainer : MonoBehaviour
    {
        [SerializeField] private SkinItemType _containedType;
        [SerializeField] private List<SkinItemSO> _items;

        [Header("Events")]
        [SerializeField] private UnityEvent<SkinItemSO> _onItemSelected;

        private int _index;

        public SkinItemSO GetSkinItemById(int id)
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
                // _items[index].IsPurchased = savedItem.IsPurchased;
            });
        }

        private void SelectItem(SkinItemSO item)
        {
            _onItemSelected.Invoke(item);
        }
    }
}
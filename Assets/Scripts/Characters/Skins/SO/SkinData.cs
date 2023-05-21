using System;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Skins
{
    [Serializable]
    public class SkinData
    {
        [SerializeField] private List<SkinItemSO> _items;

        public List<SkinItemSO> ItemsCopy => new(_items);

        public SkinItem GetByType(SkinItemType type)
        {
            return new(_items.Find(item => item.Type == type));
        }
    }
}
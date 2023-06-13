using System;
using Characters.Skins.SO;
using UnityEngine;

namespace Characters.Skins
{
    [Serializable]
    public class SkinItem
    {
        private SkinItemType _type;
        private int _id;
        private Sprite _sprite;
        private int _price;
        private bool _isPurchased;

        public SkinItemType Type => _type;
        public int Id => _id;
        public Sprite Sprite => _sprite;
        public int Price => _price;
        public bool IsPurchased => _isPurchased;

        public SkinItem(SkinItemSO so)
        {
            _type = so.Type;
            _id = so.Id;
            _sprite = so.Sprite;
            _price = so.Price;
        }
    }
}
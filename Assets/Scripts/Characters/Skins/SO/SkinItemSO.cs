using System;
using UnityEngine;

namespace Characters.Skins.SO
{
    [CreateAssetMenu(fileName = "SkinShopItem", menuName = "Skins/Skin Shop Item", order = 0)]
    public class SkinItemSO : ScriptableObject
    {
        [SerializeField] private SkinItemType _type;
        [SerializeField] private int _id;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _price;

        public SkinItemType Type => _type;
        public Sprite Sprite => _sprite;
        public int Price => _price;
        public int Id => _id;
    }

    [Serializable]
    public enum SkinItemType
    {
        Flag,
        Eyes
    }
}
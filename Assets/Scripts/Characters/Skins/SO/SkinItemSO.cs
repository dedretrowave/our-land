using UnityEngine;

namespace Characters.Skins
{
    [CreateAssetMenu(fileName = "SkinShopItem", menuName = "Skin Shop Item", order = 0)]
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

    public enum SkinItemType
    {
        Flag,
        Eyes
    }
}
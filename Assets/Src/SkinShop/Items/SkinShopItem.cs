using UnityEngine;

namespace Src.SkinShop.Items
{
    [CreateAssetMenu(fileName = "SkinShopItem", menuName = "Skin Shop Item", order = 0)]
    public class SkinShopItem : ScriptableObject
    {
        [SerializeField] private SkinShopItemCategory _category;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _price;

        public bool IsPurchased;

        public Sprite Sprite => _sprite;
        public int Price => _price;
        public SkinShopItemCategory Category => _category;
    }

    public enum SkinShopItemCategory
    {
        Flag,
        Eyes,
    }
}
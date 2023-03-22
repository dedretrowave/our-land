using Src.SkinShop.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Src.UI
{
    public class SpriteDisplay : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void SetSpriteFromSkinShopItem(SkinShopItem item)
        {
            SetSprite(item.Sprite);
        }
        
        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}
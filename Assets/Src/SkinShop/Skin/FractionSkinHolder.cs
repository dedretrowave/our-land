using System;
using Characters.Skins;
using Src.Map.Fraction;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Src.SkinShop.Skin
{
    public class FractionSkinHolder : MonoBehaviour
    {
        [SerializeField] protected Character character;

        public UnityEvent<Characters.Skins.Skin> OnSkinChanged;

        // protected Characters.Skins.Skin skin = new ();

        // public Characters.Skins.Skin Skin => skin;

        public void SetSkin(Characters.Skins.Skin newSkin)
        {
            // skin = newSkin;
            // character.Fraction.SetSkin(newSkin);
            // OnSkinChanged.Invoke(skin);
        }

        protected void Awake()
        {
            // skin = new(character.Fraction.Skin);
        }
    }
    
    [Serializable]
    public class SkinShopItemContainerToType : SerializableDictionary<SkinItemType, SkinShopItemContainer> {}
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SkinShopItemContainerToType))]
    internal class Drawer : SerializableDictionaryPropertyDrawer {}
#endif
}
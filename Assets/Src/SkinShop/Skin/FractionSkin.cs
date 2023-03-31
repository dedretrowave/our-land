using System;
using Src.Map.Fraction;
using Src.SerializableDictionary.Editor;
using Src.SkinShop.Items.Base;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Src.SkinShop.Skin
{
    public class FractionSkin : MonoBehaviour
    {
        [SerializeField] protected Character character;

        protected Items.Skin skin = new ();

        public Items.Skin Skin => skin;

        public void SetSkin(Items.Skin newSkin)
        {
            skin = newSkin;
            character.Fraction.SetSkin(newSkin);
        }

        protected void Awake()
        {
            skin = new(character.Fraction.Skin);
        }
    }
    
    [Serializable]
    public class SkinShopItemContainerToType : SerializableDictionary<SkinItemType, SkinShopItemContainer> {}
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SkinShopItemContainerToType))]
    internal class Drawer : SerializableDictionaryPropertyDrawer {}
#endif
}
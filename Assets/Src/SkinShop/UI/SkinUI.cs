using System;
using SimpleFolderIcon.SerializableDictionary.Editor;
using Src.SkinShop.Items;
using Src.SkinShop.Items.Base;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Src.SkinShop.UI
{
    public class SkinUI : MonoBehaviour
    {
        [SerializeField] private SkinUIComponent _items;

        public void SetSkin(Skin skin)
        {
            foreach (SkinItemType type in Enum.GetValues(typeof(SkinItemType)))
            {
                _items[type].sprite = skin.GetItemByType(type).Sprite;
            }
        }
    }
    
    [Serializable]
    internal class SkinUIComponent : SerializableDictionary<SkinItemType, Image> {}
    
    [CustomPropertyDrawer(typeof(SkinUIComponent))]
    internal class UIDrawer : SerializableDictionaryPropertyDrawer
    {}
}
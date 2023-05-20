using System;
using Characters.Skins;
using Src.SerializableDictionary;
using Src.SerializableDictionary.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Src.SkinShop.UI
{
    public class SkinUI : MonoBehaviour
    {
        [SerializeField] private SkinUIComponent _items = new();

        public void SetSkin(Characters.Skins.Skin skin)
        {
            foreach (SkinItemType type in Enum.GetValues(typeof(SkinItemType)))
            {
                SkinItemSO skinItem = skin.GetItemByType(type);
                
                if (skinItem == null) continue;
                
                _items[type].sprite = skinItem.Sprite;
            }
        }
    }
    
    [Serializable]
    internal class SkinUIComponent : SerializableDictionary<SkinItemType, Image> {}
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SkinUIComponent))]
    internal class UIDrawer : SerializableDictionaryPropertyDrawer
    {}
#endif
}
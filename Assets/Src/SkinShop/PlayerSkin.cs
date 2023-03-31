using System;
using System.Collections.Generic;
using Src.DI;
using Src.Map.Fraction;
using Src.Saves;
using Src.SerializableDictionary.Editor;
using Src.SkinShop.Items;
using Src.SkinShop.Items.Base;
using UnityEditor;
using UnityEngine;

namespace Src.SkinShop
{
    public class PlayerSkin : MonoBehaviour
    {
        [SerializeField] private Fraction _player;

        [SerializeField] private SkinShopItemContainerToType _skinItemContainers;

        private SkinSaveSystem _skinSave;
        private Skin _skin = new ();

        public Skin Skin
        {
            get => _skin;
            set
            {
                _skin = value;
                _player.SetSkin(value);
                _skinSave.SaveSkin(value);
            }
        }

        private void Awake()
        {
            _skinSave = DependencyContext.Dependencies.Get<SkinSaveSystem>();

            PlayerSelectedSkinData savedSkinData = _skinSave.GetPlayerSelectedSkin();

            Skin newSkin = new();
            
            if (savedSkinData.SkinItemIds.Count == 0)
            {
                _skin = new(_player.Skin);
            }
            else
            {
                foreach (var item in savedSkinData.SkinItemIds)
                {
                    newSkin.SetItem(_skinItemContainers[item.Key].GetSkinItemById(item.Value));
                }
                
                Skin = newSkin;
            }
        }
    }
    
    [Serializable]
    internal class SkinShopItemContainerToType : SerializableDictionary<SkinItemType, SkinShopItemContainer> {}
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SkinShopItemContainerToType))]
    internal class Drawer : SerializableDictionaryPropertyDrawer {}
#endif
}
using Src.DI;
using Src.Saves;
using Src.SkinShop.Items.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Src.SkinShop.Skin
{
    public class PlayerSkinHolder : FractionSkinHolder
    {
        [SerializeField] private SkinShopItemContainerToType skinItemContainers;

        private SkinSaveSystem _skinSave;

        public void SetSkin(Items.Skin newSkin)
        {
            base.SetSkin(newSkin);
            _skinSave.SaveSkin(skin);
        }
        
        private void Awake()
        {
            _skinSave = DependencyContext.Dependencies.Get<SkinSaveSystem>();
         
            PlayerSelectedSkinData savedSkinData = _skinSave.GetPlayerSelectedSkin();
         
            Items.Skin newSkin = new();
            
            if (savedSkinData == null || savedSkinData.SkinItemIds.Count == 0)
            {
                SetSkin(character.Fraction.Skin);
            }
            else
            {
                foreach (var item in savedSkinData.SkinItemIds)
                {
                    newSkin.SetItem(skinItemContainers[item.Key].GetSkinItemById(item.Value));
                }
                
                SetSkin(newSkin);
            }
        }
    }
}
using Src.DI;
using Src.Saves;
using UnityEngine;

namespace Src.SkinShop.Skin
{
    public class PlayerSkin : FractionSkin
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
            
            if (savedSkinData.SkinItemIds.Count == 0)
            {
                base.Awake();
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
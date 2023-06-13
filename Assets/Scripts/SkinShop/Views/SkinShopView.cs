using System;
using Characters.Skins;
using Characters.Skins.SO;
using Characters.View;
using UnityEngine;

namespace SkinShop.Views
{
    public class SkinShopView : MonoBehaviour
    {
        [SerializeField] private CharacterView _characterView;

        public event Action<SkinItemType> OnSelectedNext;
        public event Action<SkinItemType> OnSelectedPrev;

        public event Action OnSelected;
        public event Action OnPurchased;

        public void SetSkin(Skin skin)
        {
            _characterView.SetSkin(skin);
        }

        public void SelectNext(SkinItemType type)
        {
            OnSelectedNext?.Invoke(type);
        }
        
        public void SelectPrev(SkinItemType type)
        {
            OnSelectedPrev?.Invoke(type);
        }

        public void SelectSkin()
        {
            OnSelected?.Invoke();
        }

        public void Purchase()
        {
            OnPurchased?.Invoke();
        }
    }
}
using System;
using Characters.Skins;
using Characters.Skins.SO;
using Characters.View;
using UnityEngine;
using UnityEngine.UI;

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

        public void SelectNextEyes()
        {
            SelectNext(SkinItemType.Eyes);
        }

        public void SelectPrevEyes()
        {
            SelectPrev(SkinItemType.Eyes);
        }

        public void SelectNextFlag()
        {
            SelectNext(SkinItemType.Flag);
        }

        public void SelectPrevFlag()
        {
            SelectPrev(SkinItemType.Flag);
        }

        private void SelectNext(SkinItemType type)
        {
            OnSelectedNext?.Invoke(type);
        }
        
        private void SelectPrev(SkinItemType type)
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
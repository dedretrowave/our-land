using System;
using Characters.Skins;
using Characters.Skins.SO;
using Characters.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkinShop.Views
{
    public class SkinShopView : MonoBehaviour
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private CharacterView _characterView;

        [Header("Purchase/Select Buttons")]
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private TextMeshProUGUI _priceLabel;
        [SerializeField] private Button _adButton;

        public event Action<SkinItemType> OnSelectedNext;
        public event Action<SkinItemType> OnSelectedPrev;

        public event Action OnSelected;
        public event Action OnPurchased;
        public event Action OnGetForFree;

        public void HideButton()
        {
            _openButton.gameObject.SetActive(false);
        }

        public void ShowButton()
        {
            _openButton.gameObject.SetActive(true);
        }

        public void SetSkin(Skin skin)
        {
            _characterView.SetSkin(skin);
        }

        public void SetPrice(int price)
        {
            _priceLabel.text = price.ToString();

            if (price > 0)
            {
                SwitchButtons(ButtonsState.Purchase);
            }
            else
            {
                SwitchButtons(ButtonsState.Select);
            }
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

        public void GetForFree()
        {
            OnGetForFree?.Invoke();
        }

        private void SwitchButtons(ButtonsState state)
        {
            switch (state)
            {
                case ButtonsState.Purchase:
                    _purchaseButton.gameObject.SetActive(true);
                    _selectButton.gameObject.SetActive(false);
                    break;
                case ButtonsState.Select:
                    _purchaseButton.gameObject.SetActive(false);
                    _selectButton.gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }

    internal enum ButtonsState
    {
        Purchase,
        Select,
    }
}
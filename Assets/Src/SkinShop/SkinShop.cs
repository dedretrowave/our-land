using System;
using System.Collections.Generic;
using Src.DI;
using Src.Saves;
using Src.SkinShop.Items.Base;
using Src.SkinShop.Skin;
using UnityEngine;
using UnityEngine.Events;

namespace Src.SkinShop
{
    public class SkinShop : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Wallet.Wallet _wallet; 
        [SerializeField] private PlayerSkinHolder _playerSkin;
        [SerializeField] private Ads.Ads _ads;

        [Header("Events")]
        [SerializeField] private UnityEvent<int> _onTotalPriceChange; 
        [SerializeField] private UnityEvent<Items.Skin> _onPurchasableSkinChanged;
        [SerializeField] private UnityEvent _onItemPurchased;

        private Items.Skin _purchasableSkin = new();
        private SkinSaveSystem _skinSaveSystem;
        private int _totalPrice;

        public void Open()
        {
            List<SkinItem> playerSkinItems = new (_playerSkin.Skin.Items);
            playerSkinItems.ForEach(SetSkinItem);
            gameObject.SetActive(true);
        }

        public void SetSkinItem(SkinItem item)
        {
            _purchasableSkin.SetItem(item);
            _onPurchasableSkinChanged.Invoke(_purchasableSkin);
            RecalculatePrice();
        }

        public void BuyItems()
        {
            _wallet.Decrease(_totalPrice);
            _totalPrice = 0;
            
            PurchaseItems();
        }

        public void GetForAd()
        {
            _ads.ShowRewarded();
        }

        public void SelectSkin()
        {
            _playerSkin.SetSkin(_purchasableSkin);
        }

        private void PurchaseItems()
        {
            _purchasableSkin.Items.ForEach(item =>
            {
                item.IsPurchased = true;
                _skinSaveSystem.SaveItemPurchase(item);
            });
            _onItemPurchased.Invoke();
        }

        private void Start()
        {
            _ads.OnRewardedAdWatched.AddListener(PurchaseItems);
        }

        private void OnEnable()
        {
            _skinSaveSystem = DependencyContext.Dependencies.Get<SkinSaveSystem>();

            Items.Skin playerSkin = new(_playerSkin.Skin);

            _purchasableSkin = new(playerSkin);

            foreach (SkinItemType type in Enum.GetValues(typeof(SkinItemType)))
            {
                SetSkinItem(_purchasableSkin.GetItemByType(type));
            }
        }

        private void RecalculatePrice()
        {
            _totalPrice = _purchasableSkin.GetTotalCost();
            _onTotalPriceChange.Invoke(_totalPrice);
        }
    }
}
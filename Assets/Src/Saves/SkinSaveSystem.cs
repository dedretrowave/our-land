using System;
using System.Collections.Generic;
using Characters.Skins;
using DI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Src.Saves
{
    public class SkinSaveSystem : MonoBehaviour
    {
        [SerializeField] private string _localPath = "skins.dat";
        [SerializeField] private SaveFileHandler _handler;
        
        private CompiledSkinData _data;

        public PlayerSelectedSkinData GetPlayerSelectedSkin()
        {
            return _data.SelectedSkin ?? new PlayerSelectedSkinData();
        }

        public List<PurchasedSkinItemData> GetItemsByType(SkinItemType type)
        {
            return _data.SkinItems.ContainsKey(type) ? _data.SkinItems[type] : new ();
        }

        public void SaveSkin(Skin skin)
        {
            _data.SelectedSkin = new PlayerSelectedSkinData(skin);
            _handler.Save(_localPath, _data);
        }

        public void SaveItemPurchase(SkinItemSO item)
        {
            if (!_data.SkinItems.ContainsKey(item.Type))
            {
                _data.SkinItems[item.Type] = new List<PurchasedSkinItemData>();
                _data.SkinItems[item.Type].Add(new PurchasedSkinItemData(item));
            }
            else
            {
                PurchasedSkinItemData neededItem = _data.SkinItems[item.Type].Find(skinItem => skinItem.Id == item.Id);

                if (neededItem == null)
                {
                    item.IsPurchased = true;
                    _data.SkinItems[item.Type].Add(new PurchasedSkinItemData(item));
                }
                else
                {
                    neededItem.IsPurchased = true;
                }
            }
            
            _handler.Save(_localPath, _data);
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            DependencyContext.Dependencies.Add(typeof(SkinSaveSystem), () => this);

            _data = _handler.Load<CompiledSkinData>(_localPath) ?? new CompiledSkinData();
        }
    }

    [Serializable]
    public class CompiledSkinData
    {
        public Dictionary<SkinItemType, List<PurchasedSkinItemData>> SkinItems;
        public PlayerSelectedSkinData SelectedSkin;

        public CompiledSkinData()
        {
            SelectedSkin = new PlayerSelectedSkinData();
            SkinItems = new ();
        }

        public CompiledSkinData(SkinItemType type, List<SkinItemSO> items)
        {
            List<PurchasedSkinItemData> data = new();
            items.ForEach(item =>
            {
                data.Add(new PurchasedSkinItemData(item));
            });
        
            SkinItems[type] = data;
            SelectedSkin = new PlayerSelectedSkinData();
        }
    }

    public class PlayerSelectedSkinData
    {
        public Dictionary<SkinItemType, int> SkinItemIds = new();

        public PlayerSelectedSkinData()
        { }

        public PlayerSelectedSkinData(Skin skin)
        {
            skin.Items.ForEach(item =>
            {
                SkinItemIds[item.Type] = item.Id;
            });
        }
    }

    public class PurchasedSkinItemData
    {
        public int Id;
        public bool IsPurchased;
        
        public PurchasedSkinItemData() {}

        public PurchasedSkinItemData(SkinItemSO item)
        {
            Id = item.Id;
            IsPurchased = item.IsPurchased;
        }
    }
}
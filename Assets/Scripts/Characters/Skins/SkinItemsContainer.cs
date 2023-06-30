using System;
using System.Collections.Generic;
using Characters.Skins.SO;
using DI;
using EventBus;
using Newtonsoft.Json;
using Player.Presenter;
using Save;
using SkinShop;
using UnityEditor;
using UnityEngine;

namespace Characters.Skins
{
    public class SkinItemsContainer : MonoBehaviour
    {
        private const string LoadPath = "skins";
        
        [SerializeField] private SkinItemTypeCollectionDictionary _items = new();

        private SaveFileHandler _saveFileHandler;
        private EventBus.EventBus _eventBus;

        public void Construct()
        {
            _eventBus = EventBus.EventBus.Instance;
            _saveFileHandler = DependencyContext.Dependencies.Get<SaveFileHandler>();
            
            DependencyContext.Dependencies.Add(new(typeof(SkinItemsContainer), () => this));
            
            LoadData();
            _eventBus.AddListener<SkinItem>(EventName.ON_SKIN_IN_SHOP_PURCHASED, OnSkinDataChanged);
        }

        public void Disable()
        {
            _eventBus.RemoveListener<SkinItem>(EventName.ON_SKIN_IN_SHOP_PURCHASED, OnSkinDataChanged);
        }

        public SkinItem GetByIdAndType(SkinItemType type, int id)
        {
            return _items[type].GetById(id);
        }

        private void OnSkinDataChanged(SkinItem _)
        {
            SaveData();
        }

        private void SaveData()
        {
            List<SkinItemsData> data = new();

            foreach ((SkinItemType type,
                         SkinItemCollection collection) in _items)
            {
                List<SkinItemData> skinItems = new();

                collection.Items.ForEach(item =>
                {
                    skinItems
                        .Add(new(
                            item.Id,
                            item.Type,
                            item.IsPurchased));
                });
                
                SkinItemsData saveData = new(type, skinItems);
                
                data.Add(saveData);
            }
            
            _saveFileHandler.Save(LoadPath, data);
        }

        private void LoadData()
        {
            List<SkinItemsData> data =
                _saveFileHandler
                    .Load<List<SkinItemsData>>(LoadPath) ?? new();

            if (data.Count == 0)
            {
                foreach ((SkinItemType type, SkinItemCollection collection) in _items)
                {
                    collection.Construct();
                }

                return;
            }
            
            data.ForEach(item 
                =>
            {
                _items[item.Type].Construct(item.Items);
            });
        }
    }

    [Serializable]
    internal class SkinItemsData
    {
        private SkinItemType _type;
        private List<SkinItemData> _items;

        public SkinItemType Type => _type;
        public List<SkinItemData> Items => _items;

        [JsonConstructor]
        public SkinItemsData(SkinItemType type, List<SkinItemData> items)
        {
            _type = type;
            _items = items;
        }

        public SkinItemsData()
        {
            _type = SkinItemType.Flag;
            _items = null;
        }
    }
    
    [Serializable]
    public class SkinItemData
    {
        private int _id;
        private SkinItemType _type;
        private bool _isPurchased;

        public int Id => _id;
        public SkinItemType Type => _type;
        public bool IsPurchased => _isPurchased;

        [JsonConstructor]
        public SkinItemData(int id, int type, bool isPurchased)
        {
            _id = id;
            _type = (SkinItemType) type;
            _isPurchased = isPurchased;
        }

        public SkinItemData(int id, SkinItemType type, bool isPurchased)
        {
            _id = id;
            _type = type;
            _isPurchased = isPurchased;
        }

        public SkinItemData(SkinItem skinItem)
        {
            _id = skinItem.Id;
            _type = skinItem.Type;
            _isPurchased = skinItem.IsPurchased;
        }
    }
    
    [Serializable]
    public class SkinItemTypeCollectionDictionary : SerializableDictionary<SkinItemType, SkinItemCollection> {}
}
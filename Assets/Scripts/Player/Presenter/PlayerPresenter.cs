using System;
using System.Collections.Generic;
using Characters;
using Characters.Model;
using Characters.Skins;
using Characters.Skins.SO;
using Characters.View;
using DI;
using EventBus;
using Newtonsoft.Json;
using Save;

namespace Player.Presenter
{
    public class PlayerPresenter
    {
        private const string DataSavePath = "player";
        
        private CharacterModel _model;
        private CharacterModel _defaultModelFromSO;
        
        private CharacterView _view;

        private SaveFileHandler _saveFileHandler;

        public event Action<Skin> OnSkinChange; 

        public PlayerPresenter(CharacterView view)
        {
            CharacterContainer characterContainer = DependencyContext
                .Dependencies.Get<CharacterContainer>();

            _defaultModelFromSO = characterContainer
                .GetByFraction(Fraction.Fraction.Player);
            
            _saveFileHandler = new();
            
            _view = view;
            
            LoadData();

            _view.SetSkin(_model.Skin);
        }

        public void SetSkin(Skin skin)
        {
            _model.SetSkin(skin);
            _view.SetSkin(_model.Skin);
            SaveData(_model);
            OnSkinChange?.Invoke(_model.Skin);
        }

        public Skin GetCurrentSkin()
        {
            return _model.Skin;
        }

        private void SaveData(CharacterModel model)
        {
            CharacterData data = new(_model);
            
            _saveFileHandler.Save(DataSavePath, data);
        }

        private void LoadData()
        {
            SkinItemsContainer skinItemsContainer = DependencyContext.Dependencies.Get<SkinItemsContainer>();
            
            CharacterData data = _saveFileHandler.Load<CharacterData>(DataSavePath)
                                 ?? new();

            if (data.Id == -1)
            {
                _model = _defaultModelFromSO;
                return;
            }

            Skin playerSkin = new();
            
            data.Skin.ForEach(item 
                => playerSkin.SetItem(
                    skinItemsContainer.GetByIdAndType(item.Type, item.Id)));

            _model = new(
                data.Id,
                data.ControlledRegions,
                _defaultModelFromSO.Fraction,
                playerSkin,
                _defaultModelFromSO.Color,
                _defaultModelFromSO.AllowsDivisionGeneration,
                _defaultModelFromSO.IsPlayerControlled
                );
        }
    }
    
    [Serializable]
    public class CharacterData
    {
        private int _id;
        private List<int> _controlledRegions;
        private List<SkinItemData> _skin = new();

        public int Id => _id;
        public List<int> ControlledRegions => new(_controlledRegions);
        public List<SkinItemData> Skin => new (_skin);

        public CharacterData()
        {
            _id = -1;
            _controlledRegions = new();
            _skin = new();
        }

        [JsonConstructor]
        public CharacterData(
            int id,
            IEnumerable<int> controlledRegions,
            IEnumerable<SkinItemData> skin)
        {
            _id = id;
            _controlledRegions = new(controlledRegions);
            _skin = new(skin);
        }

        public CharacterData(CharacterModel model)
        {
            _id = model.Id;
            _controlledRegions = new(model.ControlledRegionIds);
            
            model.Skin.IterateThroughItems(Assign);

            void Assign(SkinItem item)
            {
                _skin.Add(new(item));
            }
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
}
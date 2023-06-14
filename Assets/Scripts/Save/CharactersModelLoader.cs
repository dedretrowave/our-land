using System;
using System.Collections.Generic;
using Characters;
using Characters.Model;
using Characters.Skins;
using Characters.Skins.SO;
using DI;
using Newtonsoft.Json;

namespace Save
{
    public class CharactersModelLoader
    {
        private const string LocalPath = "characters";

        private SaveFileHandler _saveFileHandler;
        private CharacterContainer _characterContainer;

        private List<CharacterData> _characters = new();

        public CharactersModelLoader()
        {
            _saveFileHandler = new SaveFileHandler();

            _characters = _saveFileHandler.Load<List<CharacterData>>(LocalPath)
                ?? new();
        }

        public List<CharacterData> GetData()
        {
            return _characters;
        }

        public void SaveCharacter(CharacterModel characterModel)
        {
            CharacterData existingRegion = _characters.Find(
                model => model.Id == characterModel.Id);
            CharacterData characterData = new(characterModel);
            
            if (existingRegion == null)
            {
                _characters.Add(characterData);
            }
            else
            {
                _characters.Remove(existingRegion);
                _characters.Add(characterData);
            }

            _saveFileHandler.Save(LocalPath, _characters);
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
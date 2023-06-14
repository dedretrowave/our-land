using System.Collections.Generic;
using Characters.Model;
using Characters.Skins;
using Characters.SO;
using DI;
using Save;
using UnityEngine;

namespace Characters
{
    public class CharacterContainer : MonoBehaviour
    {
        [SerializeField] private List<CharacterSO> _characterSOs;
        
        private SkinItemsContainer _skinItemsContainer;
        
        private List<CharacterModel> _characters = new();

        public void Construct()
        {
            _skinItemsContainer = DependencyContext.Dependencies.Get<SkinItemsContainer>();
        }
        
        private void Awake()
        {
            _characterSOs.ForEach(characterSO => Add(new(characterSO)));
            
            DependencyContext.Dependencies.Add(new(typeof(CharacterContainer), () => this));
        }

        public void Add(CharacterModel character)
        {
            if (!_characters.Contains(character))
            {
                _characters.Add(character);
            }
        }

        public void CreateModelsFromData(List<CharacterData> charactersData)
        {
            _characters.Clear();

            charactersData.ForEach(data =>
            {
                CharacterSO so = _characterSOs.Find(character => character.Id == data.Id);
                Skin selectedSkin = new();
                
                data.Skin.ForEach(item =>
                {
                    SkinItem skinItem = _skinItemsContainer.GetByIdAndType(item.Type, item.Id);
                    selectedSkin.SetItem(skinItem);
                });

                _characters.Add(new(
                    data.Id,
                    data.ControlledRegions,
                    so.Fraction,
                    selectedSkin,
                    so.Color,
                    so.AllowsDivisionGeneration,
                    so.IsPlayerControlled
                    ));
            });
        }

        public CharacterModel GetById(int id)
        {
            return _characters.Find(character => character.Id == id);
        }

        public CharacterModel GetByFraction(Fraction.Fraction fraction)
        {
            return _characters.Find(character => character.Fraction == fraction);
        }
    }
}
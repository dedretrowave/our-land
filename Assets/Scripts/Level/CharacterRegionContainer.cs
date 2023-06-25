using System;
using System.Collections.Generic;
using Characters.Model;
using DI;
using Region.Views;
using UnityEngine;

namespace Level
{
    public class CharacterRegionContainer : MonoBehaviour
    {
        private Dictionary<CharacterModel, List<RegionView>> _characterRegions = new();

        public event Action<CharacterModel> OnCharacterLost;

        public void Add(CharacterModel character, RegionView regionView)
        {
            if (_characterRegions.ContainsKey(character))
            {
                _characterRegions[character].Add(regionView);
            }
            else
            {
                _characterRegions[character] = new() {regionView};
            }

            regionView.OnOwnerChange += MoveRegionToCharacter;
        }

        public List<RegionView> GetRegionsByCharacter(CharacterModel character)
        {
            return _characterRegions[character];
        }

        private void MoveRegionToCharacter(RegionView region, CharacterModel oldOwner, CharacterModel newOwner)
        {
            _characterRegions[oldOwner].Remove(region);

            if (_characterRegions[oldOwner].Count == 0)
            {
                OnCharacterLost?.Invoke(oldOwner);
            }
            
            _characterRegions[newOwner].Add(region);
        }

        private void OnDisable()
        {
            foreach (var characterRegion in _characterRegions)
            {
                characterRegion.Value.ForEach(region => region.OnOwnerChange -= MoveRegionToCharacter);
            }
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new(typeof(CharacterRegionContainer), () => this));
        }
    }
}
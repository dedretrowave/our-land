using System;
using System.Collections.Generic;
using Characters.Base;
using DI;
using Region.Views;
using UnityEngine;

namespace Level
{
    public class CharacterRegionContainer : MonoBehaviour
    {
        private Dictionary<Character, List<RegionView>> _characterRegions = new();

        public event Action<Character> OnCharacterLost;

        public void Add(Character character, RegionView regionView)
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

        public List<RegionView> GetRegionsByCharacter(Character character)
        {
            return _characterRegions[character];
        }

        private void MoveRegionToCharacter(RegionView region, Character oldOwner, Character newOwner)
        {
            if (_characterRegions[oldOwner].Count != 0 || _characterRegions.ContainsKey(oldOwner))
            {
                _characterRegions[oldOwner].Remove(region);
            }

            if (_characterRegions[oldOwner].Count == 0)
            {
                OnCharacterLost?.Invoke(oldOwner);
            }

            if (_characterRegions.ContainsKey(newOwner))
            {
                _characterRegions[newOwner].Add(region);
            }
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
using System;
using System.Collections.Generic;
using Characters.Base;
using DI;
using Level.Region;
using UnityEngine;

namespace Level
{
    public class CharacterRegionContainer : MonoBehaviour
    {
        private Dictionary<Character, List<RegionInstaller>> _characterRegions = new();

        public event Action<Character> OnCharacterLost; 

        public void Add(Character character, RegionInstaller regionPresenter)
        {
            if (_characterRegions.ContainsKey(character))
            {
                _characterRegions[character].Add(regionPresenter);
            }
            else
            {
                _characterRegions[character] = new() {regionPresenter};
            }

            regionPresenter.OnOwnerChange += MoveRegionToCharacter;
        }

        public List<RegionInstaller> GetRegionsByCharacter(Character character)
        {
            return _characterRegions[character];
        }

        private void MoveRegionToCharacter(RegionInstaller region, Character oldOwner, Character newOwner)
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
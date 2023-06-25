using System;
using System.Collections.Generic;
using Characters;
using Characters.Model;
using DI;
using Region;
using UnityEditor;
using UnityEngine;

namespace Level
{
    public class LevelConfig : MonoBehaviour
    {
        [SerializeField] private int _reward;
        [SerializeField] private FractionRegionDictionary _fractionRegions;

        private List<CharacterModel> _characters = new();

        public List<CharacterModel> Characters => _characters;

        public int Reward => _reward;

        public void Init(CharacterModel enemy)
        {
            CharacterRegionContainer characterRegionContainer
                = DependencyContext.Dependencies.Get<CharacterRegionContainer>();
            CharacterContainer characterFactory 
                = DependencyContext.Dependencies.Get<CharacterContainer>();

            foreach (var fractionRegion 
                     in _fractionRegions)
            {
                fractionRegion.Value.List.ForEach(region =>
                {
                    CharacterModel regionOwner;
                    
                    regionOwner = fractionRegion.Key == Fraction.Fraction.Enemy ? 
                        enemy : characterFactory.GetByFraction(fractionRegion.Key);

                    if (!_characters.Contains(regionOwner)) 
                        _characters.Add(regionOwner);
                    
                    region.Construct(regionOwner);
                    characterRegionContainer.Add(regionOwner, region.View);
                });
            }
        }

        public void Disable()
        {
            foreach ((Fraction.Fraction _, Regions regions) in _fractionRegions)
            {
                regions.List.ForEach(region => region.Disable());
            }
        }
    }
    
    
    [Serializable]
    public class Regions
    { 
        public List<RegionInstaller> List;
    }
    
    [Serializable]
    public class FractionRegionDictionary : SerializableDictionary<Fraction.Fraction, Regions> {}
}
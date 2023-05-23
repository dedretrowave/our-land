using System.Collections.Generic;
using Characters.Base;
using DI;
using Level.Models;
using Level.Region;
using UnityEngine;

namespace Level.Enemy
{
    public class EnemyAI
    {
        private Character _character;

        private List<RegionInstaller> _ownedRegions;
        private Dictionary<Character, List<RegionInstaller>> _enemyRegions = new();

        private LevelModel _levelModel;

        public EnemyAI(Character character, LevelModel model)
        {
            _levelModel = model;
            
            _character = character;

            CharacterRegionContainer characterRegionContainer =
                DependencyContext.Dependencies.Get<CharacterRegionContainer>();
            
            Debug.Log($"{_character.Id} IS HERE");
            Debug.Log($"AND MY ENEMIES ARE");

            _ownedRegions = characterRegionContainer.GetRegionsByCharacter(_character);
            
            _levelModel.CharactersOnLevel.ForEach(character =>
            {
                if (!character.Equals(_character))
                {
                    _enemyRegions[character] = 
                        characterRegionContainer.GetRegionsByCharacter(character);
                    Debug.Log($"{character.Id}");
                }
            });
            
            Debug.Log("==============");
        }
    }
}
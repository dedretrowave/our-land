using System;
using System.Collections.Generic;
using Characters.Base;
using Characters.SO;
using DI;
using Level.Enemy;
using Level.Models;
using Level.Presenters;
using Level.Region;
using UnityEditor;
using UnityEngine;

namespace Level
{
    public class LevelInstaller : MonoBehaviour
    {
        [SerializeField] private CharacterSORegionDictionary _characterSORegions;

        private LevelModel _levelModel;
        
        private CharacterRegionContainer _characterRegionContainer;
        
        private LevelProgress _levelProgress;
        private List<EnemyAI> _enemyAis = new();

        private List<Coroutine> _enemyAIsCoroutines = new();

        // TODO: Check if can refactor
        public void Construct()
        {
            List<Character> characters = new();
            
            _characterRegionContainer = DependencyContext.Dependencies.Get<CharacterRegionContainer>();

            foreach (var characterSoRegion in _characterSORegions)
            {
                Character characterFromSO = new(characterSoRegion.Key);

                characters.Add(characterFromSO);

                characterSoRegion.Value.List.ForEach(region =>
                {
                    region.Construct(characterFromSO);
                    _characterRegionContainer.Add(characterFromSO, region.View);
                });
            }
            
            _levelModel = new LevelModel(characters);
            
            _levelModel.CharactersOnLevel.ForEach(character =>
            {
                if (character.Fraction != Fraction.Fraction.Enemy) return;
                
                _enemyAis.Add(new (character, _levelModel));
            });
            
            _enemyAis.ForEach(ai => _enemyAIsCoroutines.Add(StartCoroutine(ai.StartAttacking())));

            _levelProgress = new(_levelModel);

            _characterRegionContainer.OnCharacterLost += _levelProgress.ChangeStatusAfterCharacterLost;
            _levelProgress.OnStatusChange += TryFinishWithStatus;
        }

        private void TryFinishWithStatus(LevelStatus status)
        {
            if (status == LevelStatus.InProgress) return;

            switch (status)
            {
                case LevelStatus.Win:
                    Debug.Log("WIN!!!!");
                    break;
                case LevelStatus.Lose:
                    Debug.Log("LOSE((((");
                    break;
                default:
                    return;
            }
        }

        private void OnDisable()
        {
            _characterRegionContainer.OnCharacterLost -= _levelProgress.ChangeStatusAfterCharacterLost;
            _levelProgress.OnStatusChange -= TryFinishWithStatus;
            
            _enemyAIsCoroutines.ForEach(ai => StopCoroutine(ai));
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new(typeof(LevelInstaller), () => this));
        }
    }

    [Serializable]
    internal class Regions
    { 
        public List<RegionInstaller> List;
    }
    
#if UNITY_EDITOR
    [Serializable]
    internal class CharacterSORegionDictionary : SerializableDictionary<CharacterSO, Regions> {}
    [CustomPropertyDrawer(typeof(CharacterSORegionDictionary))]
    internal class CharacterSORegionDictionaryUI : SerializableDictionaryPropertyDrawer {}
#endif
}
using System;
using System.Collections.Generic;
using Characters.Model;
using DI;
using Level.Enemy;
using Level.Models;
using Level.Presenters;
using UnityEngine;

namespace Level
{
    public class LevelInstaller : MonoBehaviour
    {
        [SerializeField] private LevelConfig _levelConfig;

        private CharacterRegionContainer _characterRegionContainer;
        
        private LevelProgress _levelProgress;
        private List<EnemyAI> _enemyAis = new();

        private List<Coroutine> _enemyAIsCoroutines = new();

        public event Action<LevelStatus> OnEnd;
        
        public void Construct(LevelConfig config, LevelModel levelModel)
        {
            _levelConfig = config;
            _characterRegionContainer = DependencyContext.Dependencies.Get<CharacterRegionContainer>();
            
            levelModel.CharactersOnLevel.ForEach(character =>
            {
                if (character.Fraction != Fraction.Fraction.Enemy) return;
                
                _enemyAis.Add(new (character, levelModel));
            });
            
            _enemyAis.ForEach(ai => _enemyAIsCoroutines.Add(
                StartCoroutine(ai.StartAttacking())));

            _levelProgress = new(levelModel);

            _characterRegionContainer.OnCharacterLost += _levelProgress.ChangeStatusAfterCharacterLost;
            _levelProgress.OnEndWithStatus += InvokeLevelEndAndUnsubscribe;
        }

        private void InvokeLevelEndAndUnsubscribe(LevelStatus status)
        {
            OnEnd?.Invoke(status);
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            _characterRegionContainer.OnCharacterLost -= _levelProgress.ChangeStatusAfterCharacterLost;
            _levelProgress.OnStatusChange -= InvokeLevelEndAndUnsubscribe;
            
            _enemyAIsCoroutines.ForEach(ai => StopCoroutine(ai));
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new(typeof(LevelInstaller), () => this));
        }
    }
}
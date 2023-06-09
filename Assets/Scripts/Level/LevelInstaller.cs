using System;
using System.Collections.Generic;
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

        private LevelModel _levelModel;

        private CharacterRegionContainer _characterRegionContainer;
        
        private LevelProgress _levelProgress;
        private List<EnemyAI> _enemyAis = new();

        private List<Coroutine> _enemyAIsCoroutines = new();

        public event Action OnStarted;
        public event Action OnWin;
        public event Action<int> OnWinWithReward; 
        public event Action OnLose;
        public event Action OnEnd;
        
        public void Construct(LevelConfig config, LevelModel levelModel)
        {
            _levelModel = levelModel;
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
            
            OnStarted?.Invoke();
        }

        private void InvokeLevelEndAndUnsubscribe(LevelStatus status)
        {
            switch (status)
            {
                case LevelStatus.Win:
                    OnWinWithReward?.Invoke(_levelModel.Reward);
                    OnWin?.Invoke();
                    OnEnd?.Invoke();
                    break;
                case LevelStatus.Lose:
                    OnLose?.Invoke();
                    OnEnd?.Invoke();
                    break;
                case LevelStatus.InProgress:
                default:
                    return;
            }

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
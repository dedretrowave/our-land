using System.Collections.Generic;
using Src.DI;
using Src.Regions.Containers;
using Src.Saves;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Levels.Level
{
    public class LevelProgress : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private int _id;
        
        [Header("Containers")]
        [SerializeField] private RegionContainer _playerContainer;
        [SerializeField] private List<RegionContainer> _enemyContainers;

        [Header("Events")]
        [SerializeField] private UnityEvent<LevelCompletionState> _onCompletionStatusChange = new();
        [SerializeField] private UnityEvent<LevelCompletionState> _onInitWithStatus = new();

        private LevelCompletionState _status = LevelCompletionState.Incomplete;
        private int _defeatedEnemies;
        
        private SaveSystem _save;

        public LevelCompletionState Status => _status;

        public void BindEvents()
        {
            _playerContainer.OnEmpty.AddListener(Fail);
            _enemyContainers.ForEach(enemy =>
            {
                enemy.OnEmpty.AddListener(ProceedToCompletion);
            });
        }

        private void Start()
        {
            _save = DependencyContext.Dependencies.Get<SaveSystem>();

            _status = _save.GetLevelById(_id)?.Status ?? LevelCompletionState.Incomplete;

            _onInitWithStatus.Invoke(_status);
        }

        private void ProceedToCompletion()
        {
            _defeatedEnemies++;

            if (_defeatedEnemies == _enemyContainers.Count)
            {
                Complete();
            }
        }

        private void Fail()
        {
            FinishLevelWithStatus(LevelCompletionState.Incomplete);
        }

        private void Complete()
        {
            FinishLevelWithStatus(LevelCompletionState.Complete);
        }

        private void FinishLevelWithStatus(LevelCompletionState newStatus)
        {
            _status = newStatus;
            
            _save.SaveLevel(new LevelData(_id, _status));

            _onCompletionStatusChange.Invoke(_status);
        }
    }
}
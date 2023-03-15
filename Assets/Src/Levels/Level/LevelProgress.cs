using System.Collections.Generic;
using Src.Regions.Containers;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Levels.Level
{
    public class LevelProgress : MonoBehaviour
    {
        [Header("Containers")]
        [SerializeField] private RegionContainer _playerContainer;
        [SerializeField] private List<RegionContainer> _enemyContainers;

        [Header("Events")]
        [SerializeField] private UnityEvent<LevelCompletionState> _onCompletionStatusChange = new();
        [SerializeField] private UnityEvent _onComplete = new();
        [SerializeField] private UnityEvent _onFail = new();

        private LevelCompletionState _status = LevelCompletionState.Incomplete;
        private Transform _levelInstance;
        private int _defeatedEnemies;

        public void Init(Transform instance)
        {
            _levelInstance = instance;
            _playerContainer.OnEmpty.AddListener(Fail);
            _enemyContainers.ForEach(enemy =>
            {
                enemy.OnEmpty.AddListener(ProceedToCompletion);
            });
        }

        private void Start()
        {
            _onCompletionStatusChange.Invoke(_status);
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
            _onFail.Invoke();
        }

        private void Complete()
        {
            FinishLevelWithStatus(LevelCompletionState.Complete);
            _onComplete.Invoke();
        }

        private void FinishLevelWithStatus(LevelCompletionState newStatus)
        {
            _status = newStatus;

            _onCompletionStatusChange.Invoke(_status);
            Destroy(_levelInstance.gameObject);
        }
    }
}
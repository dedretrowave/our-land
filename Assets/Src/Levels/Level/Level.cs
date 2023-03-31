using System.Collections.Generic;
using Src.DI;
using Src.Levels.Level.Initialization;
using Src.Levels.Level.UI;
using Src.Map.Regions.Containers;
using Src.Saves;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Levels.Level
{
    public class Level : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private LevelReward _reward;
        [SerializeField] private LevelSpawner _spawner;
        [SerializeField] private LevelUI _ui;
        
        [Header("Parameters")]
        [SerializeField] private int _id;
        
        [Header("Containers")]
        [SerializeField] private RegionContainer _playerContainer;
        [SerializeField] private List<RegionContainer> _enemyContainers;

        [Header("Events")]
        [SerializeField] private UnityEvent<Level> _onFinish = new();
        [SerializeField] private UnityEvent<Level> _onStatusChange = new();

        private LevelCompletionState _status = LevelCompletionState.Incomplete;
        private int _defeatedEnemies;
        
        private PlayerDataSaveSystem _save;

        public LevelCompletionState Status => _status;
        public LevelReward Reward => _reward;

        public void Init()
        {
            BindEvents();
            _spawner.Spawn();
            _ui.Hide();
        }

        public void ChangeStatusToIncomplete()
        {
            SetStatus(LevelCompletionState.Incomplete);
        }

        public void BindEvents()
        {
            _playerContainer.OnEmpty.AddListener(Fail);
            _enemyContainers.ForEach(enemy =>
            {
                enemy.OnEmpty.AddListener(ProceedToCompletion);
            });
        }

        private void ClearContainers()
        {
            _playerContainer.Clear();
            _enemyContainers.ForEach(container => container.Clear());
        }

        private void Start()
        {
            _save = DependencyContext.Dependencies.Get<PlayerDataSaveSystem>();

            _status = _save.GetLevelById(_id)?.Status ?? LevelCompletionState.Incomplete;

            _onStatusChange.Invoke(this);
        }

        private void ProceedToCompletion()
        {
            _defeatedEnemies++;

            if (_defeatedEnemies >= _enemyContainers.Count)
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
            SetStatus(newStatus);

            _onFinish.Invoke(this);
            
            ClearContainers();
        }
        
        private void SetStatus(LevelCompletionState status)
        {
            _status = status;
            _save.SaveLevel(_id, new LevelData(_status));
            _onStatusChange.Invoke(this);
        }
    }
}
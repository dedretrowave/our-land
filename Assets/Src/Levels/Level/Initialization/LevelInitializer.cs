using System;
using Src.DI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Src.Levels.Level.Initialization
{
    public class LevelInitializer : MonoBehaviour
    {
        [Header("Containers")]
        [SerializeField] private Transform _selectedLevelContainer;
        [SerializeField] private Transform _levelsContainer;

        public UnityEvent<Transform> OnLevelStarted;
        public UnityEvent<LevelCompletionState> OnLevelFinished;
        public UnityEvent OnLevelCompleted;
        public UnityEvent OnLevelFailed;

        private Transform _selectedLevel;

        public void InitializeLevel(Transform level)
        {
            _selectedLevel = level;
            _selectedLevel.SetParent(_selectedLevelContainer);
            OnLevelStarted.Invoke(_selectedLevel);
        }

        public void EndLevel(LevelCompletionState state)
        {
            _selectedLevel.SetParent(_levelsContainer);
            OnLevelFinished.Invoke(state);

            switch (state)
            {
                case LevelCompletionState.Complete:
                    OnLevelCompleted.Invoke();
                    break;
                case LevelCompletionState.Incomplete:
                    OnLevelFailed.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void Start()
        {
            DependencyContext.Dependencies.Add(typeof(LevelInitializer), () => this);
        }
    }
}
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

        public UnityEvent<Level> OnLevelStarted;
        public UnityEvent<Level> OnLevelFinished;

        private Transform _selectedLevel;

        public void InitializeLevel(Level level)
        {
            _selectedLevel = level.transform;
            _selectedLevel.SetParent(_selectedLevelContainer);
            OnLevelStarted.Invoke(level);
        }

        public void EndLevel(Level level)
        {
            _selectedLevel.SetParent(_levelsContainer);
            OnLevelFinished.Invoke(level);
        }

        private void Start()
        {
            DependencyContext.Dependencies.Add(typeof(LevelInitializer), () => this);
        }
    }
}
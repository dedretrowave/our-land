using DI;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Levels.Level.Initialization
{
    public class LevelInitializer : MonoBehaviour
    {
        [Header("Containers")]
        [SerializeField] private Transform _selectedLevelContainer;
        [SerializeField] private Transform _levelsContainer;

        public UnityEvent<Level> OnLevelStarted;
        public UnityEvent<Level> OnLevelFinished;

        private Level _selectedLevel;

        public void InitializeLevel(Level level)
        {
            if (_selectedLevel != null) return;
            
            _selectedLevel = level;
            _selectedLevel.transform.SetParent(_selectedLevelContainer);
            level.Init();
            OnLevelStarted.Invoke(level);
        }

        public void EndLevel(Level level)
        {
            if (_selectedLevel == null) return;
            
            _selectedLevel.transform.SetParent(_levelsContainer);
            OnLevelFinished.Invoke(level);
            _selectedLevel = null;
        }

        private void Start()
        {
            DependencyContext.Dependencies.Add(typeof(LevelInitializer), () => this);
        }
    }
}
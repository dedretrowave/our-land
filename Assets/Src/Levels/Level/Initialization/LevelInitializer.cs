using Src.DI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Src.Levels.Level.Initialization
{
    public class LevelInitializer : MonoBehaviour
    {
        [FormerlySerializedAs("_container")]
        [Header("Containers")]
        [SerializeField] private Transform _selectedLevelContainer;
        [SerializeField] private Transform _levelsContainer;

        public UnityEvent<Transform> OnLevelStarted;
        public UnityEvent<LevelCompletionState> OnLevelFinished;
        
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
        }

        private void Start()
        {
            DependencyContext.Dependencies.Add(typeof(LevelInitializer), () => this);
        }
    }
}
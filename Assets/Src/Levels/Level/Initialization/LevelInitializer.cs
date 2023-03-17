using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Src.Levels.Level.Initialization
{
    public class LevelInitializer : MonoBehaviour, IPointerClickHandler
    {
        [Header("Prefab")]
        [SerializeField] private Transform _levelPrefab;

        [Header("Components")]
        [SerializeField] private LevelProgress _progress;

        [SerializeField] private UnityEvent<Transform> _onLevelStarted;

        private bool _isStarted;

        public void EndLevel()
        {
            _isStarted = false;
        }

        public void StartLevel()
        {
            if (_isStarted) return;

            Transform instance = Instantiate(_levelPrefab, transform);
            _onLevelStarted.Invoke(instance.transform);
            _isStarted = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_progress.Status == LevelCompletionState.Complete)
            {
                enabled = false;
                return;
            }

            StartLevel();
        }
    }
}
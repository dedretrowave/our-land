using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Src.Levels.Level.Initialization
{
    public class LevelInitializer : MonoBehaviour, IPointerClickHandler
    {
        
        [Header("Prefab")]
        [SerializeField] private Transform _levelPrefab;

        [SerializeField] private UnityEvent<Transform> _onLevelStarted;

        private bool _isStarted;

        public void StartLevel()
        {
            if (_isStarted) return;

            Transform instance = Instantiate(_levelPrefab, transform);
            _onLevelStarted.Invoke(instance.transform);
            _isStarted = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            StartLevel();
        }
    }
}
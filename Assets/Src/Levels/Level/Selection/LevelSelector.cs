using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Src.Levels.Level.Selection
{
    public class LevelSelector : MonoBehaviour, IPointerUpHandler
    {
        [SerializeField] private Level _level;

        [SerializeField] private UnityEvent<Transform> _onLevelStarted;

        private bool _isStarted;

        private void StartLevel()
        {
            if (_isStarted) return;
            
            Transform prefab = _level.Prefab;
            
            Instantiate(prefab, transform);
            _onLevelStarted.Invoke(prefab);
            _isStarted = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StartLevel();
        }
    }
}
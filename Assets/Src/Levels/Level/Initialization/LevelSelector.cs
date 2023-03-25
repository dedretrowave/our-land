using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Src.Levels.Level.Initialization
{
    public class LevelSelector : MonoBehaviour, IPointerClickHandler
    {
        [Header("Components")]
        [SerializeField] private LevelProgress _progress;

        [Header("Events")]
        [SerializeField] private UnityEvent<Transform> _onLevelSelected;

        private bool _isStarted;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_progress.Status == LevelCompletionState.Complete)
            {
                enabled = false;
                return;
            }
            
            _onLevelSelected.Invoke(transform);
        }
    }
}
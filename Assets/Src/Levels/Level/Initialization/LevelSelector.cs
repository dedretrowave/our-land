using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Src.Levels.Level.Initialization
{
    public class LevelSelector : MonoBehaviour, IPointerClickHandler
    {
        [Header("Components")]
        [SerializeField] private Level _level;

        [Header("Events")]
        [SerializeField] private UnityEvent<Level> _onLevelSelected;

        private bool _isStarted;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_level.Status == LevelCompletionState.Complete)
            {
                enabled = false;
                return;
            }
            
            _onLevelSelected.Invoke(_level);
        }
    }
}
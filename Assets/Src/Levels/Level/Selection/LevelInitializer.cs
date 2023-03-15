using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;

namespace Src.Levels.Level.Selection
{
    public class LevelInitializer : MonoBehaviour, IPointerUpHandler
    {
        [Header("UIComponent")]
        [SerializeField] private Button _button;
        
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

        public void OnPointerUp(PointerEventData eventData)
        {
            _button.gameObject.SetActive(true);
        }
    }
}
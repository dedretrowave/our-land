using EventBus;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Camera
{
    public class CameraDrag : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _xBorderValue = 350f;
        [SerializeField] private float _yBorderValue = 370f;

        private EventBus.EventBus _eventBus;
        private PlayerInput _playerInput;

        private bool _isFrozen;

        private void Start()
        {
            _eventBus = EventBus.EventBus.Instance; 
            
            EnhancedTouchSupport.Enable();
            
            _eventBus.AddListener(EventName.ON_LEVEL_STARTED, Freeze);
            _eventBus.AddListener(EventName.ON_LEVEL_ENDED, UnFreeze);
        }

        private void OnDisable()
        { 
            EnhancedTouchSupport.Disable();
            
            _eventBus.RemoveListener(EventName.ON_LEVEL_STARTED, Freeze);
            _eventBus.RemoveListener(EventName.ON_LEVEL_ENDED, UnFreeze);
        }

        private void Freeze() => _isFrozen = true;
        private void UnFreeze() => _isFrozen = false;

        private void Update()
        {
            if (_isFrozen) return;
            
            if (Touch.activeFingers.Count == 1)
            {
                TouchMove(Touch.activeTouches[0]);
            }

            if (Input.GetMouseButton(0))
            {
                PointerMove(Mouse.current.delta.ReadValue());
            }
        }

        private void TouchMove(Touch touch)
        {
            if (touch.phase != TouchPhase.Moved) return;

            Vector3 deltaPosition = new Vector3(
                -touch.delta.normalized.x,
                -touch.delta.normalized.y,
                0) * (Time.deltaTime * _speed);

            Move(deltaPosition);
        }

        private void PointerMove(Vector3 newPosition)
        {
            Vector3 deltaPosition = new Vector3(
                -newPosition.normalized.x,
                -newPosition.normalized.y,
                0) * (Time.deltaTime * _speed);

            Move(deltaPosition);
        }

        private void Move(Vector3 position)
        {
            Vector3 newPosition = transform.position + position * _speed;

            if (Mathf.Abs(newPosition.x) >= _xBorderValue
                || Mathf.Abs(newPosition.y) >= _yBorderValue) return;

            transform.position = newPosition;
        }
    }
}
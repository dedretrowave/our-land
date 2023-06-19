using System.Linq;
using UnityEngine;
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

        private void Start()
        {
            EnhancedTouchSupport.Enable();
        }

        private void OnDisable()
        {
            EnhancedTouchSupport.Disable();
        }

        private void Update()
        {
            if (Touch.activeFingers.Count == 1)
            {
                MoveCamera(Touch.activeTouches[0]);
            }
        }

        private void MoveCamera(Touch touch)
        {
            if (touch.phase != TouchPhase.Moved) return;

            Vector3 deltaPosition = new Vector3(
                -touch.delta.normalized.x,
                -touch.delta.normalized.y,
                0) * (Time.deltaTime * _speed);

            Vector3 newPosition = transform.position + deltaPosition * _speed;

            if (Mathf.Abs(newPosition.x) >= _xBorderValue
                || Mathf.Abs(newPosition.y) >= _yBorderValue) return;

            transform.position = newPosition;
        }
    }
}
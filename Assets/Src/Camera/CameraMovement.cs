using Src.Controls;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Src.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _cam;
        
        [Header("Parameters")]
        [SerializeField] private float _speed = 150f;
        
        [Header("Borders")]
        [SerializeField] private float _yBorderValue;
        [SerializeField] private float _xBorderValue;
        
        private const float SpeedCorrection = 100f;
        
        private Vector3 _touchStart;
        private int _groundZ = -10;
        private bool _isFrozen;
        
        public void Freeze()
        {
            _isFrozen = true;
        }
        
        public void UnFreeze()
        {
            _isFrozen = false;
        }
        
        private void Start()
        {
            PlayerControls.Instance.OnFingerMove.AddListener(OnDrag);
            PlayerControls.Instance.OnFingerDown.AddListener(OnFingerDown);
        }
        
        private void OnFingerDown(Finger finger)
        {
            _touchStart = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);;
            _touchStart.z = UnityEngine.Camera.main.transform.position.z;
        }
        
        private void OnDrag(Finger finger)
        {
            if (_isFrozen) return;

            Vector3 newPosition = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition) - UnityEngine.Camera.main.transform.position;
            newPosition.z = 0f;

            if (Mathf.Abs(newPosition.x) >= _xBorderValue ||
                Mathf.Abs(newPosition.y) >= _yBorderValue) return;

            _cam.transform.position = _touchStart - newPosition;
        }
    }
}
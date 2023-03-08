using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Src.Controls
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Camera _cam;

        [Header("Borders")]
        [SerializeField] private float _yBorderValue;
        [SerializeField] private float _xBorderValue;
        
        private Vector3 _touchStart;
        private int _groundZ;

        private void Start()
        {
            EnhancedTouchSupport.Enable();
            TouchSimulation.Enable();
            
            Touch.onFingerMove += OnDrag;
            Touch.onFingerDown += OnFingerDown;
        }

        private void OnFingerDown(Finger finger)
        {
            _touchStart = GetWorldPosition(0);
        }

        private void OnDrag(Finger finger)
        {
            
            Vector3 direction = _touchStart - GetWorldPosition(_groundZ);
            Vector3 newCameraPosition = _cam.transform.position + direction;
            
            if (Mathf.Abs(newCameraPosition.x) >= _xBorderValue ||
                Mathf.Abs(newCameraPosition.z) >= _yBorderValue) return;
            
            _cam.transform.position = newCameraPosition;
        }
        
        private Vector3 GetWorldPosition(float z)
        {
            Ray mousePos = _cam.ScreenPointToRay(Input.mousePosition);
            Plane ground = new(Vector3.up, new Vector3(0,z,0));
            float distance;
            ground.Raycast(mousePos, out distance);
            return mousePos.GetPoint(distance);
        }
    }
}
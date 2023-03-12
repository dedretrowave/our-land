using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Src.Controls
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Camera _cam;

        [Header("Parameters")]
        [SerializeField] private float _speed = 3f;

        [Header("Borders")]
        [SerializeField] private float _yBorderValue;
        [SerializeField] private float _xBorderValue;
        
        private Vector3 _touchStart;
        private int _groundZ;

        private void Start()
        {
            PlayerControls.Instance.OnFingerMove.AddListener(OnDrag);
            PlayerControls.Instance.OnFingerDown.AddListener(OnFingerDown);
        }

        private void OnFingerDown(Finger finger)
        {
            _touchStart = GetWorldPosition(_groundZ);
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
            Ray mousePosition = _cam.ScreenPointToRay(UnityEngine.Input.mousePosition);
            Plane ground = new(Vector3.forward, new Vector3(0,0,z));
            ground.Raycast(mousePosition, out float distance);
            return mousePosition.GetPoint(distance);
        }
    }
}
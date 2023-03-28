using Src.Levels.Level;
using UnityEngine;

namespace Src.Camera
{
    public class CameraOnLevelFocus : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private CameraMovement _movement;

        [Header("Parameters")]
        [SerializeField] private float _cameraOnFocusZPosition = -25f;

        private Vector3 _cameraDefaultPosition;

        public void Focus(Level level)
        {
            Focus(level.transform);
        }

        public void Focus(Transform level)
        {
            _camera.transform.position = new Vector3(level.position.x, level.position.y, _cameraOnFocusZPosition);
            _movement.Freeze();
        }

        public void UnFocus()
        {
            _camera.transform.position = _cameraDefaultPosition;
            _movement.UnFreeze();
        }

        private void Start()
        {
            _cameraDefaultPosition = _camera.transform.position;
        }
    }
}
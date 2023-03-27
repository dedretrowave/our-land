using Src.Levels.Level;
using UnityEngine;

namespace Src.Camera
{
    public class CameraOnLevelFocus : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private CameraMovement _movement;

        private Vector3 _cameraDefaultPosition;

        public void Focus(Level level)
        {
            Focus(level.transform);
        }

        public void Focus(Transform level)
        {
            _camera.transform.position = new Vector3(level.position.x, level.position.y, 0f);
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
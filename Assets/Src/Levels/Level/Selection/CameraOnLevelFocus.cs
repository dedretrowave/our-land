using Src.Controls;
using UnityEngine;

namespace Src.Levels.Level.Selection
{
    public class CameraOnLevelFocus : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraMovement _movement;

        private Vector3 _cameraDefaultPosition;

        public void Focus(Transform level)
        {
            _camera.transform.position = new Vector3(level.localPosition.x, level.localPosition.y, 50f);
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
using UnityEngine;

namespace Src.Levels.Level.Selection
{
    public class LevelZoom : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Camera _camera;

        public void ZoomIntoView(Transform level)
        {
            Vector3 transformLocalPosition = _canvas.transform.localPosition;
            transformLocalPosition.z = 0f;
            _canvas.transform.localPosition = transformLocalPosition;
            _camera.transform.position = new Vector3(level.position.x, level.position.y, _camera.transform.position.z);
        }
    }
}
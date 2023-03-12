using UnityEngine;

namespace Src.Levels.Level.Selection
{
    public class LevelZoom : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Camera _camera;

        public void ZoomIntoView(Transform level)
        {
            _canvas.transform.localScale = new Vector3(.2f, .2f, .2f);
            _camera.transform.position = new Vector3(level.position.x, level.position.y, _camera.transform.position.z);
        }
    }
}
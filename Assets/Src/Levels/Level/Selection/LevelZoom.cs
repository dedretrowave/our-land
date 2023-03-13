using UnityEngine;

namespace Src.Levels.Level.Selection
{
    public class LevelZoom : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        public void ZoomIntoView(Transform level)
        {
            _camera.transform.position = new Vector3(level.position.x, level.position.y, 50f);
        }
    }
}
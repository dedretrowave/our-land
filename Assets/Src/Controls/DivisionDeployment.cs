using Level.Region.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Src.Controls
{
    public class DivisionDeployment : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private RegionView _view;
        
        [Header("Parameters")]
        [SerializeField] private LayerMask _selectionLayer;

        private const float RaycastDepth = 1000;

        public void OnPointerDown(PointerEventData eventData)
        { }

        public void OnPointerUp(PointerEventData eventData)
        {
            Transform hitTransform = GetRaycastHit(eventData);

            if (hitTransform == null) return;

            _view.Release(hitTransform);
        }

        private void Awake()
        {
            _view = GetComponent<RegionView>();
        }

        private Transform GetRaycastHit(PointerEventData eventData)
        {
            return Select(eventData.position);
        }

        private Transform Select(Vector2 mousePosition)
        {
            Physics.Raycast(UnityEngine.Camera.main.ScreenPointToRay(mousePosition),
                out RaycastHit hit,
                RaycastDepth,
                _selectionLayer);

            Transform hitTransform = hit.transform;

            return hitTransform;
        }
    }
}
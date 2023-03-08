using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Src.Controls
{
    public class DragSelection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Parameters")]
        [SerializeField] private LayerMask _selectionLayer;
        
        private const float Raycastdepth = 1000;

        [SerializeField] private UnityEvent<Transform> _onPointerDownEvent;
        [SerializeField] private UnityEvent<Transform> _onPointerUpEvent;

        public void OnPointerDown(PointerEventData eventData)
        {
            Transform hitTransform = GetRaycastHit(eventData);

            if (hitTransform == null) return;

            _onPointerDownEvent.Invoke(hitTransform);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Transform hitTransform = GetRaycastHit(eventData);

            if (hitTransform == null) return;

            _onPointerUpEvent.Invoke(hitTransform);
        }

        private Transform GetRaycastHit(PointerEventData eventData)
        {
            return Select(eventData.position);
        }

        private Transform Select(Vector2 mousePosition)
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition),
                out RaycastHit hit,
                Raycastdepth,
                _selectionLayer);

            Transform hitTransform = hit.transform;

            return hitTransform;
        }
    }
}
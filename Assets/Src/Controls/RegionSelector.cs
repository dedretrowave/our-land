using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Src.Controls
{
    public class RegionSelector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Parameters")]
        [SerializeField] private LayerMask _regionLayer;
        
        private const float Raycastdepth = 1000;

        [SerializeField] private UnityEvent<Transform> _onPointerDownEvent;
        [SerializeField] private UnityEvent<Transform> _onPointerUpEvent;

        public void OnPointerDown(PointerEventData eventData)
        {
            Transform hitTransform = GetRaycastHit(eventData);
            
            _onPointerDownEvent.Invoke(hitTransform);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Transform hitTransform = GetRaycastHit(eventData);

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
                _regionLayer);

            Transform hitTransform = hit.transform;

            return hitTransform;
        }
    }
}
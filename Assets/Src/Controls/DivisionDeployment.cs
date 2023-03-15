using Src.Regions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Src.Controls
{
    public class DivisionDeployment : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Components")]
        [SerializeField] private DivisionMover _mover;
        
        [Header("Parameters")]
        [SerializeField] private LayerMask _selectionLayer;

        private const float RaycastDepth = 1000;
        
        public void OnPointerDown(PointerEventData eventData)
        { }

        public void OnPointerUp(PointerEventData eventData)
        {
            Transform hitTransform = GetRaycastHit(eventData);

            if (hitTransform == null) return;

            _mover.MoveTo(hitTransform);
        }

        private void Start()
        { }

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
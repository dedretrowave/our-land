using Src.Divisions.Base;
using Src.Regions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Src.Divisions.Controls
{
    public class DivisionMovementArranger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Parameters")]
        [SerializeField] private LayerMask _regionLayer;
        [Header("Components")]
        [SerializeField] private Division _division;
        
        private const float Raycastdepth = 1000;
        
        public void OnPointerDown(PointerEventData eventData) { }

        public void OnPointerUp(PointerEventData eventData)
        {
            SelectRegion(eventData.position);
        }

        private void SelectRegion(Vector2 mousePosition)
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out RaycastHit hit, Raycastdepth, _regionLayer);
            
            Transform hitTransform = hit.transform;

            if (hitTransform != null)
            {
                // _division.MoveToCaptureRegion(hitTransform.GetComponent<Region>(),
                //     CalculateEndpointPosition(hitTransform));
                
                Debug.Log(CalculateEndpointPosition(hitTransform));
            }
        }

        private Vector3 CalculateEndpointPosition(Transform hitTransform)
        {
            const float distanceFactor = .1f;

            return hitTransform.position - (hitTransform.position - transform.localPosition) * distanceFactor;
        }
    }
}
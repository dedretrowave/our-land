using Src.Interfaces;
using Src.Regions.Structures;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Src.Divisions.Controls
{
    public class DivisionControls : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Parameters")]
        [SerializeField] private LayerMask _regionLayer;
        [Header("Components")]
        [SerializeField] private DivisionBase _divisionBase;
        
        private const float Raycastdepth = 1000;
        
        public void OnPointerDown(PointerEventData eventData) { }

        public void OnPointerUp(PointerEventData eventData)
        {
            SelectRegion(eventData.position);
        }

        private void SelectRegion(Vector2 mousePosition)
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out RaycastHit hit, Raycastdepth);
            
            Transform hitTransform = hit.transform;

            if (hitTransform == null) return;
            
            _divisionBase.SendDivision(hitTransform.GetComponent<IMovementTargetable>().GetPosition());
        }
    }
}
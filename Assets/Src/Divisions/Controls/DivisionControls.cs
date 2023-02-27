using Src.Regions;
using Src.Regions.Fraction;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Src.Divisions.Controls
{
    public class DivisionControls : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Parameters")]
        [SerializeField] private LayerMask _regionLayer;
        
        private Region _region;
        private const float Raycastdepth = 1000;

        public void OnPointerDown(PointerEventData eventData)
        {
            Transform hitTransform = Select(eventData.position);

            if (hitTransform == null) return;

            Region region = hitTransform.GetComponent<Region>();

            if (region.Owner.Fraction == Fraction.Player)
            {
                _region = region;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_region == null) return;
            
            Transform hitTransform = Select(eventData.position);

            if (hitTransform == null || hitTransform.Equals(_region.transform)) return;

            Division division = _region.DeployDivision();
            
            division.Deploy(hitTransform.GetComponent<Region>().GetPosition());
            _region = null;
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
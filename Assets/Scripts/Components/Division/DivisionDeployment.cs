using Region.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Components.Division
{
    public class DivisionDeployment : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private GarrisonView _view;

        private const float RaycastDepth = 1000;

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Transform hitTransform = GetRaycastHit(eventData);

            if (hitTransform == null) return;

            _view.Release(hitTransform);
        }

        private void Awake()
        {
            _view = GetComponent<GarrisonView>();
        }

        private Transform GetRaycastHit(PointerEventData eventData)
        {
            return Select(eventData.position);
        }

        private Transform Select(Vector2 mousePosition)
        {
            Physics.Raycast(UnityEngine.Camera.main.ScreenPointToRay(mousePosition),
                out RaycastHit hit,
                RaycastDepth);

            Transform hitTransform = hit.transform;

            return hitTransform;
        }
    }
}
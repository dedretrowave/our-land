using Src.Map.Garrisons.Defenders.Deployment;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Src.Map.Garrisons.Defenders.UI
{
    public class DefendersDeployerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private DefendersDeployer _deployer;
        [SerializeField] private LayerMask _layer;

        public void OnPointerDown(PointerEventData eventData)
        { }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            Physics.Raycast(UnityEngine.Camera.main.ScreenPointToRay(eventData.position),
                out RaycastHit hit,
                1000,
                _layer);

            Transform hitTransform = hit.transform;
            
            _deployer.Deploy(hitTransform);
        }
    }
}
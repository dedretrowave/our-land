using System;
using Src.Regions;
using Src.Regions.Structures;
using UnityEngine;

namespace Src.Controls
{
    public class DivisionMover : MonoBehaviour
    { 
        [SerializeField] private GarrisonBase _base;

        public void MoveTo(Transform directionPoint)
        {
            if (_base == null || directionPoint.transform.Equals(_base.transform)) return;

            Vector3 targetRegion;
            
            try
            {
                targetRegion = directionPoint.GetComponent<Region>().GetPosition();
            }
            catch (Exception e)
            {
                return;
            }

            _base.DeployDivisions(targetRegion);
        }
    }
}
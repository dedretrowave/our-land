using System;
using Src.Map.Regions;
using Src.Map.Regions.Structures;
using UnityEngine;

namespace Src.Controls
{
    public class DivisionMover : MonoBehaviour
    { 
        [SerializeField] private GarrisonBase _base;

        public void MoveTo(Transform directionPoint)
        {
            if (_base == null || directionPoint.transform.Equals(_base.transform)) return;

            Map.Regions.Region targetRegion;
            
            try
            {
                targetRegion = directionPoint.GetComponent<Map.Regions.Region>();
            }
            catch (Exception)
            {
                return;
            }

            _base.DeployDivisions(targetRegion);
        }
    }
}
using System;
using Src.Divisions.Divisions;
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

            Division division = _base.DeployDivision();

            division.Deploy(targetRegion);
        }
    }
}
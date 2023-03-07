using System;
using Src.Divisions.Divisions;
using Src.Regions;
using Src.Regions.Fraction;
using UnityEngine;

namespace Src.Controls
{
    public class DivisionMover : MonoBehaviour
    {
        private Region _region;
        
        public void SetRegion(Transform directionPoint)
        {
            Region region = directionPoint.GetComponent<Region>();

            if (region == null) return;

            if (region.Owner.Fraction == Fraction.Player)
            {
                _region = region;
            }
        }

        public void Deploy(Transform directionPoint)
        {
            if (_region == null || directionPoint.transform.Equals(_region.transform)) return;

            Vector3 targetRegion;

            try
            {
                targetRegion = directionPoint.GetComponent<Region>().GetPosition();
            }
            catch (Exception e)
            {
                return;
            }

            Division division = _region.DeployDivision();

            division.Deploy(targetRegion);
            _region = null;
        }
    }
}
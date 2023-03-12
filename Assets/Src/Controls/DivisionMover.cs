using System;
using Src.Divisions.Divisions;
using Src.Fraction;
using Src.Regions;
using UnityEngine;

namespace Src.Controls
{
    public class DivisionMover : MonoBehaviour
    {
        [SerializeField] private Character _character;
        
        private Region _originRegion;
        
        public void SetRegion(Transform directionPoint)
        {
            Region region = directionPoint.GetComponent<Region>();

            if (region == null) return;

            if (region.Owner.Fraction.Equals(_character.Fraction))
            {
                _originRegion = region;
            }
        }

        public void Deploy(Transform directionPoint)
        {
            if (_originRegion == null || directionPoint.transform.Equals(_originRegion.transform)) return;

            Vector3 targetRegion;

            try
            {
                targetRegion = directionPoint.GetComponent<Region>().GetPosition();
            }
            catch (Exception e)
            {
                return;
            }

            Division division = _originRegion.DeployDivision();

            division.Deploy(targetRegion);
            _originRegion = null;
        }
    }
}
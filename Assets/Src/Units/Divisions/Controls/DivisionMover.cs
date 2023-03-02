using Src.Regions;
using Src.Regions.Fraction;
using UnityEngine;

namespace Src.Units.Divisions.Controls
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

        public void DeployDivision(Transform directionPoint)
        {
            if (_region == null || directionPoint.transform.Equals(_region.transform)) return;

            Division division = _region.DeployDivision();
            
            division.Deploy(directionPoint.GetComponent<Region>().GetPosition());
            _region = null;
        }
    }
}
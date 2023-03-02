using Src.Divisions;
using Src.Regions.Fraction;
using Src.Regions.Structures;
using Src.Units.Divisions;
using UnityEngine;

namespace Src.Regions
{
    public class Region : MonoBehaviour
    {
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private DivisionBase _base;

        public RegionOwner Owner => _owner;
        public DivisionBase Base => _base;

        public Division DeployDivision()
        {
            return _base.DeployDivision();
        }
        
        public Vector3 GetPosition()
        {
            return _base.transform.position;
        }
    }
}
using Src.Divisions;
using Src.Regions.Combat;
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
        [SerializeField] private RegionDefence _defence;

        public RegionOwner Owner => _owner;
        public DivisionBase Base => _base;
        public RegionDefence Defence => _defence;

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
using Src.Global;
using Src.Regions.Combat;
using Src.Regions.Containers;
using Src.Regions.Fraction;
using Src.Regions.Structures;
using Src.Units.Divisions;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Regions
{
    public class Region : MonoBehaviour
    {
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private GarrisonBase _base;
        [SerializeField] private RegionDefence _defence;

        [SerializeField] private UnityEvent<Fraction.Fraction> _onOwnerChange;

        private RegionContainer _container;
        private RegionDistributor _distributor;

        public RegionOwner Owner => _owner;
        public RegionDefence Defence => _defence;

        public void SetContainer(RegionContainer container)
        {
            if (_container != null) _container.RemoveRegion(this);
            
            container.AddRegion(this);
            _container = container;
        }

        public Division DeployDivision()
        {
            return _base.DeployDivision();
        }
        
        public Vector3 GetPosition()
        {
            return _base.transform.position;
        }

        public void SetOwner(Fraction.Fraction newOwner)
        {
            _owner.Change(newOwner);
            _onOwnerChange.Invoke(_owner.Fraction);
            _distributor.DistributeRegion(this, _owner.Fraction);
        }

        private void Start()
        {
            _distributor = DependencyContext.Dependencies.Get<RegionDistributor>();
            SetOwner(_owner.Fraction);
        }
    }
}
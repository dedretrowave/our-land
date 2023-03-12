using Src.Divisions.Divisions;
using Src.Divisions.Garrison;
using Src.Global;
using Src.Regions.Combat;
using Src.Regions.Containers;
using Src.Regions.Structures;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Regions
{
    public class Region : MonoBehaviour
    {
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private GarrisonBase _base;
        [SerializeField] private RegionDefence _defence;
        [SerializeField] private DivisionsGenerator _generator;

        [SerializeField] private UnityEvent<Fraction.Fraction> _onOwnerChange = new();

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

        public Vector3 GetPosition()
        {
            return _base.transform.position;
        }

        public void SetOwner(Fraction.Fraction newOwner)
        {
            if (newOwner == _owner.Fraction) return;
            
            _owner.SetFraction(newOwner);
            _onOwnerChange.Invoke(_owner.Fraction);
            _distributor.DistributeRegion(this, _owner.Fraction);
        }
        
        private void SwitchDivisionGeneratorByFraction()
        {
            if (!_owner.Fraction.AllowsDivisionGeneration) return;

            _generator.enabled = true;
            _generator.StartGeneration();
        }

        private void Start()
        {
            _distributor = DependencyContext.Dependencies.Get<RegionDistributor>();
            _onOwnerChange.Invoke(_owner.Fraction);
            _distributor.DistributeRegion(this, _owner.Fraction);
            SwitchDivisionGeneratorByFraction();
        }
    }
}
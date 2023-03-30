using Src.Controls;
using Src.DI;
using Src.Map.Garrisons;
using Src.Map.Regions.Combat;
using Src.Map.Regions.Containers;
using Src.Map.Regions.Structures;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Map.Regions
{
    public class Region : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private GarrisonBase _base;
        [SerializeField] private RegionDefence _defence;
        [SerializeField] private DivisionsGenerator _generator;
        [SerializeField] private DivisionDeployment _deployment;

        [Header("Events")]
        public UnityEvent<Fraction.Fraction> OnOwnerChange = new();

        private RegionContainer _container;
        private RegionDistributor _distributor;

        public RegionOwner Owner => _owner;
        public RegionDefence Defence => _defence;
        public GarrisonBase Base => _base;

        public void SetContainer(RegionContainer container)
        {
            if (_container != null) _container.RemoveRegion(this);
            
            _container = container;
            _container.AddRegion(this);
        }

        public Vector3 GetPosition()
        {
            return _base.transform.position;
        }

        public void SetOwner(Fraction.Fraction newOwner)
        {
            if (newOwner == _owner.Fraction) return;
            
            _owner.SetFraction(newOwner);
            OnOwnerChange.Invoke(_owner.Fraction);
            _distributor.DistributeRegion(this, _owner.Fraction);
            SwitchGeneratorByFraction();
            SwitchDeploymentByFraction();
        }

        private void SwitchDeploymentByFraction()
        {
            if (!_owner.Fraction.IsPlayerControlled)
            {
                _deployment.enabled = false;
                return;
            }

            _deployment.enabled = true;
        }
        
        private void SwitchGeneratorByFraction()
        {
            if (!_owner.Fraction.AllowsDivisionGeneration)
            {
                _generator.StopGeneration();
                _generator.enabled = false;
                return;
            }

            _generator.enabled = true;
            _generator.StartGeneration();
        }

        private void Start()
        {
            _distributor = DependencyContext.Dependencies.Get<RegionDistributor>();
            OnOwnerChange.Invoke(_owner.Fraction);
            _distributor.DistributeRegion(this, _owner.Fraction);
            SwitchGeneratorByFraction();
            SwitchDeploymentByFraction();
        }
    }
}
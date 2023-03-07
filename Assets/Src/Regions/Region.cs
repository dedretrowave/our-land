using Src.Divisions.Divisions;
using Src.Divisions.Garrison;
using Src.Global;
using Src.Models.Region;
using Src.Regions.Combat;
using Src.Regions.Containers;
using Src.Regions.Fraction;
using Src.Regions.Structures;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Src.Regions
{
    public class Region : MonoBehaviour
    {
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private GarrisonBase _base;
        [SerializeField] private RegionDefence _defence;
        [SerializeField] private DivisionsGenerator _generator;
        [SerializeField] private RawImage _image;
        [SerializeField] private Garrison _garrison;

        [SerializeField] private UnityEvent<Fraction.Fraction> _onOwnerChange = new();

        private RegionContainer _container;
        private RegionDistributor _distributor;
        private RegionData _data;

        public RegionOwner Owner => _owner;
        public RegionDefence Defence => _defence;

        public void SetData(RegionData data)
        {
            _data = data;
        }

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
            if (newOwner == _owner.Fraction) return;
            
            _owner.SetFraction(newOwner);
            _onOwnerChange.Invoke(_owner.Fraction);
            _distributor.DistributeRegion(this, _owner.Fraction);
            SwitchDivisionGeneratorByFraction();
        }

        //TODO: убрать это нахуй, тк земля не может стать нейтральной, переделать шобы генератор спавнился как только земля перестаёт быть нейтральной
        private void SwitchDivisionGeneratorByFraction()
        {
            switch (_owner.Fraction)
            {
                case Fraction.Fraction.Neutral:
                    _generator.StopGeneration();
                    _generator.enabled = false;
                    break;
                case Fraction.Fraction.Player:
                case Fraction.Fraction.Enemy:
                default:
                    _generator.enabled = true;
                    _generator.StartGeneration();
                    break;
            }
        }

        private void Start()
        {
            _distributor = DependencyContext.Dependencies.Get<RegionDistributor>();
            Init();
        }
        
        private void Init()
        {
            transform.position = _data.Position;
            _garrison.Init(_data.GarrisonInitialNumber);
            _generator.Init(_data.GenerationRate);
            _image.texture = _data.Image.texture;
            
            SetOwner(_data.Fraction);
        }
    }
}
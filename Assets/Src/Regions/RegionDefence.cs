using Src.Divisions;
using Src.Regions.Fraction;
using UnityEngine;

namespace Src.Regions
{
    public class RegionDefence : MonoBehaviour
    {
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private DivisionBase _base;

        private Fraction.Fraction _regionClaimer;

        public void ChangeOwner()
        {
            _owner.Change(_regionClaimer);
        }

        public void RegisterDivision(Division division)
        {
            _regionClaimer = division.Fraction;
            
            Debug.Log(division.Fraction != _owner.Fraction);
            
            if (division.Fraction != _owner.Fraction)
            {
                _base.TakeDamage(division.Number);
            }
            else
            {
                _base.TakeSupplies(division.Number);
            }
        }

        private void Start()
        {
            _regionClaimer = _owner.Fraction;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Division division)) return;

            RegisterDivision(division);
        }
    }
}
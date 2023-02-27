using Src.Divisions;
using Src.Regions.Fraction;
using Src.Regions.Structures;
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

        private void RegisterDivision(Division division)
        {
            if (division.ParentBase.Equals(_base)) return;
            
            _regionClaimer = division.Fraction;

            if (division.Fraction != _owner.Fraction)
            {
                _base.TakeDamage(division.Number);
            }
            else
            {
                _base.TakeSupplies(division.Number);
            }
            
            Destroy(division.gameObject);
        }

        private void Awake()
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
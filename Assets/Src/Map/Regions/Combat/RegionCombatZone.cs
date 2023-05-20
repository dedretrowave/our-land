using Src.Map.Fraction;
using Src.Map.Garrisons.Divisions;
using Src.Map.Regions.Structures;
using UnityEngine;

namespace Src.Map.Regions.Combat
{
    public class RegionCombatZone : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Region _region;
        [SerializeField] private GarrisonBase _base;

        [Header("Defenders")]
        [SerializeField] private RegionDefence _defence;

        private Character _regionClaimer;

        public void ChangeOwner()
        {
            _region.SetOwner(_regionClaimer);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Division division)) return;

            HandleDivision(division);
        }

        private void HandleDivision(Division division)
        {
            if (division.ParentBase.Equals(_base) || !division.TargetRegion.Equals(_region)) return;

            _regionClaimer = division.Fraction;

            if (division.Fraction != _region.Owner.Fraction)
            {
                _defence.ApplyDefence(division);
                _base.TakeDamage();
            }
            else
            {
                _base.TakeSupply();
            }
            
            Destroy(division.gameObject);
        }

        private void Start()
        {
            _regionClaimer = _region.Owner.Fraction;
        }
    }
}
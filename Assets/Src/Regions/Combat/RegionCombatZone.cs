using Src.Divisions.Divisions;
using Src.Regions.Structures;
using UnityEngine;

namespace Src.Regions.Combat
{
    public class RegionCombatZone : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Region _region;
        [SerializeField] private GarrisonBase _base;

        [Header("Defenders")]
        [SerializeField] private RegionDefence _defence;

        private Fraction.Fraction _regionClaimer;

        public void ChangeOwner()
        {
            _region.SetOwner(_regionClaimer);
            _base.SwapOffenceAndSupply();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Division division)) return;

            HandleDivision(division);
        }

        private void HandleDivision(Division division)
        {
            if (division.ParentBase.Equals(_base)) return;

            _regionClaimer = division.Fraction;

            if (division.Fraction != _region.Owner.Fraction)
            {
                _defence.ApplyDefence(division);
                _base.AddOffence(division.Amount);
            }
            else
            {
                _base.AddSupply(division.Amount);
            }
            
            Destroy(division.gameObject);
        }

        private void Start()
        {
            _regionClaimer = _region.Owner.Fraction;
        }
    }
}
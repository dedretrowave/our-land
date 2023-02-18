using Src.Divisions;
using Src.Divisions.Base;
using Src.Divisions.Combat.Base;
using Src.Regions.RegionCombat;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Src.Regions
{
    public class Region : MonoBehaviour
    { 
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private RegionCombatZone _combatZone;
        [SerializeField] private Health _health;

        private Fraction _newClaimerFraction;

        public void SetNewClaimer(Fraction fraction)
        {
            _newClaimerFraction = fraction;
        }

        public void TakeDamage()
        {
            _health.Decrease();
        }

        public void ChangeOwner()
        {
            _owner.ChangeFraction(_newClaimerFraction);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Division division)) return;
            
            if (division.Fraction == _owner.Fraction)
            {
                _combatZone.AddDefence(division);
            }
            else
            {
                _combatZone.AddOffence(division);
            }
        }
    }
}
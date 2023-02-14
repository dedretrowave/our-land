using Src.Divisions;
using Src.Divisions.Base;
using Src.Divisions.Combat.Base;
using Src.Regions.RegionCombat;
using UnityEngine;

namespace Src.Regions
{
    public class Region : MonoBehaviour
    { 
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private RegionCombatZone _combatZone;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Division enemy) && enemy.Fraction != _owner.Fraction)
            {
                _combatZone.EngageInCombat(enemy.GetComponent<Attacker>());
            }
        }
    }
}
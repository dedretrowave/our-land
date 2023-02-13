using Src.Divisions.Base;
using UnityEngine;

namespace Src.Regions
{
    public class Region : MonoBehaviour
    {
        [SerializeField] private Fraction _fraction;
        [SerializeField] private RegionDefence.RegionCombatZone _combatZone;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"{other.name} SPOTTED");
            if (other.TryGetComponent(out Division enemy) && enemy.Fraction != _fraction)
            {
                _combatZone.SetEnemy(enemy);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            _combatZone.EngageCombat();
        }
    }
}
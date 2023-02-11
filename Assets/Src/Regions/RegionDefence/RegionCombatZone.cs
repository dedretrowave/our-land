using System.Collections.Generic;
using Src.Divisions.Attack.Base;
using UnityEngine;

namespace Src.Regions.RegionDefence
{
    public class RegionCombatZone : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private List<Attacker> _attackers;

        public void StartCombat(Attacker enemy)
        {
            _attackers.ForEach(attacker =>
            {
                attacker.Attack(enemy);
            });
        }
    }
}
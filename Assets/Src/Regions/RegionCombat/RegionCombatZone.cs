using System.Collections.Generic;
using Src.Divisions.Base;
using Src.Divisions.Combat.Base;
using UnityEngine;

namespace Src.Regions.RegionCombat
{
    public class RegionCombatZone : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private List<Attacker> _attackers;

        private Attacker _enemy;

        public void AddDivision(Attacker attacker)
        {
            _attackers.Add(attacker);
        }

        public void AddDivision(Division division)
        {
            _attackers.Add(division.GetComponent<Attacker>());
        }

        public void RemoveDivision(Attacker attacker)
        {
            _attackers.Remove(attacker);
        }
        
        public void RemoveDivision(Division division)
        {
            _attackers.Remove(division.GetComponent<Attacker>());
        }

        public void EngageInCombat(Attacker enemy)
        {
            _attackers.ForEach(attacker =>
            {
                attacker.AttackTarget(enemy.GetComponent<Attacker>());
            });
            //
            // enemy.AttackTarget(_attackers[0]);
        }
    }
}
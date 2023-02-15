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
        
        private List<Attacker> _enemies = new();

        public void AddDefenceDivision(Division division)
        {
            AddDefenceDivision(division.GetComponent<Attacker>());
        }
        
        public void AddDefenceDivision(Attacker attacker)
        {
            _attackers.Add(attacker);

            _enemies.ForEach(enemy => 
            {
                Fight(attacker, enemy);
            });
        }
        
        public void RemoveDefenceDivision(Division division)
        {
            RemoveDefenceDivision(division.GetComponent<Attacker>());
        }

        public void RemoveDefenceDivision(Attacker attacker)
        {
            _attackers.Remove(attacker);
        }

        public void EngageInCombat(Attacker enemy)
        {
            _enemies.Add(enemy);

            _attackers.ForEach(attacker =>
            {
                Fight(attacker, enemy);
            });
        }

        private void Fight(Attacker attacker, Attacker enemy)
        {
            attacker.AttackTarget(enemy);
            enemy.AttackTarget(attacker);
        }
    }
}
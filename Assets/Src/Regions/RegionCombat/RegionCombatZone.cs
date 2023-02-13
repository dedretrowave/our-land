using System.Collections.Generic;
using Src.Divisions.Attack.Base;
using Src.Divisions.Base;
using Src.Regions.RegionDivisions;
using UnityEngine;

namespace Src.Regions.RegionCombat
{
    public class RegionCombatZone : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private List<DivisionBase> _bases;
        [SerializeField] private List<Attacker> _attackers;

        private Attacker _enemy;

        public void AddAttacker(Division division)
        {
            _attackers.Add(division.GetComponent<Attacker>());
        }

        public void SetEnemy(Division enemy)
        {
            _enemy = enemy.Attacker;
        }

        public void EngageInCombat()
        {
            if (_enemy == null) return;
            
            _attackers.ForEach(attacker =>
            {
                attacker.Attack(_enemy);
                _enemy.Attack(attacker);
            });
        }
    }
}
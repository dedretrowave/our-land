using System.Collections.Generic;
using Src.Divisions.Attack.Base;
using Src.Divisions.Base;
using UnityEngine;

namespace Src.Regions.RegionDefence
{
    public class RegionCombatZone : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private List<Attacker> _attackers;

        private Attacker _enemy;

        public void SetEnemy(Division enemy)
        {
            _enemy = enemy.Attacker;
        }

        public void EngageCombat()
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
using System.Collections.Generic;
using Src.Divisions.Base;
using Src.Divisions.Combat.Base;
using UnityEngine;
using UnityEngine.Serialization;

namespace Src.Regions.RegionCombat
{
    public class RegionCombatZone : MonoBehaviour
    {
        [SerializeField] private Region _region;
        [SerializeField] private List<Attacker> _defenders;
        
        private List<Attacker> _enemies = new();

        public void AddDefenceDivision(Division division)
        {
            AddDefenceDivision(division.Attacker);
        }
        
        public void AddDefenceDivision(Attacker attacker)
        {
            _defenders.Add(attacker);

            _enemies.ForEach(enemy => 
            {
                Fight(attacker, enemy);
            });
        }
        
        public void RemoveDefenceDivision(Division division)
        {
            RemoveDefenceDivision(division.Attacker);
        }

        public void RemoveDefenceDivision(Attacker attacker)
        {
            _defenders.Remove(attacker);

            if (_defenders.Count == 0)
            {
                _enemies.ForEach(enemy => enemy.ProceedAfterEnemiesDefeated(_region));
            }
        }

        public void EngageInCombat(Attacker enemy)
        {
            _enemies.Add(enemy);
            
            if (_defenders.Count == 0)
            {
                _enemies.ForEach(enemy => enemy.ProceedAfterEnemiesDefeated(_region));
                return;
            }

            _defenders.ForEach(defender =>
            {
                Fight(defender, enemy);
            });
        }

        private void Fight(Attacker attacker, Attacker enemy)
        {
            attacker.AttackTarget(enemy);
            enemy.AttackTarget(attacker);
        }
    }
}
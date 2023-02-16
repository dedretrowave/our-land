using System;
using System.Collections.Generic;
using Src.Divisions.Base;
using Src.Divisions.Conquer;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Regions.RegionCombat
{
    public class RegionCombatZone : MonoBehaviour
    {
        [SerializeField] private Region _region;
        [SerializeField] private List<Division> _defenders;
        
        private List<Division> _offenders = new();

        [HideInInspector] public UnityEvent OnDefenceDivisionsDefeated;
        [HideInInspector] public UnityEvent OnDefenceDivisionsRestored;

        public void AddDefence(Division defender)
        {
            IncreaseDivisionAddIfNull(defender, _defenders);
            defender.OnNumberEqualsZero.AddListener(() => RemoveDefender(defender));
            OnDefenceDivisionsRestored.Invoke();

            EngageInCombat();
        }

        public void AddOffence(Division offender)
        {
            IncreaseDivisionAddIfNull(offender, _offenders);
            offender.OnNumberEqualsZero.AddListener(() => RemoveOffender(offender));

            if (offender.TryGetComponent(out Conquer conquer))
            {
                conquer.SetDependencies(_region, this);
            }

            EngageInCombat();
        }

        private void IncreaseDivisionAddIfNull(Division division, List<Division> list)
        {
            Type divisionAttackerType = division.AttackerType;
            Division divisionInListAttacker = list.Find(item => item.AttackerType == divisionAttackerType);

            if (divisionInListAttacker == null)
            {
                list.Add(division);
            }
            else
            {
                divisionInListAttacker.IncreaseNumber(division.Number);
            }
        }

        private void RemoveDefender(Division division)
        {
            _defenders.Remove(division);
            
            if (_defenders.Count == 0)
            {
                OnDefenceDivisionsDefeated.Invoke();
            }
        }

        private void RemoveOffender(Division division)
        {
            _offenders.Remove(division);
        }

        private void EngageInCombat()
        {
            Attack(_offenders, _defenders);
            Attack(_defenders, _offenders);
        }

        private void Attack(List<Division> attackers, List<Division> enemies)
        {
            attackers.ForEach(attacker =>
            {
                Debug.Log($"{attacker} ADDING {enemies.Count} ENEMIES");
                attacker.AttackEnemy(enemies);
            });
        }
    }
}
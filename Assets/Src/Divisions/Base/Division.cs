using System;
using System.Collections.Generic;
using Src.Divisions.Combat.Base;
using Src.Divisions.Number;
using Src.Regions;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Divisions.Base
{
    public class Division : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private Movement.Movement _movement;
        [SerializeField] private DivisionNumber _number;
        [SerializeField] private Attacker _attacker;

        [Header("Events")]
        public UnityEvent OnNumberEqualsZero;
        public UnityEvent<int> OnNumberChange;
        public UnityEvent OnDamageTaken;

        public Fraction Fraction => _owner.Fraction;
        public int Number => _number.Number;
        public Type AttackerType => _attacker.GetType();

        public void IncreaseNumber(int number = 1)
        {
            _number.Increase(number);
        }

        public void AttackEnemy(List<Division> enemies)
        {
            enemies.ForEach(AttackEnemy);
        }

        public void SetInitialParameters(Fraction fraction = Fraction.Neutral, int number = 0)
        {
            _owner.ChangeFraction(fraction);
            _number.Increase(number);
        }
        
        private void AttackEnemy(Division enemy)
        {
            _attacker.AttackTarget(enemy.GetComponent<Attacker>());
        }

        private void Start()
        {
            _number.OnNumberEqualsZero.AddListener(OnNumberEqualsZero.Invoke);
            _number.OnNumberChange.AddListener(OnNumberChange.Invoke);
            _attacker.OnDamageTaken.AddListener(OnDamageTaken.Invoke);
        }
    }
}
using Src.Divisions.Attack.Base;
using Src.Divisions.Number;
using Src.Regions;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Divisions.Base
{
    public class Division : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Fraction _fraction;
        [SerializeField] private Movement.Movement _movement;
        [SerializeField] private DivisionNumber _number;
        [SerializeField] private Attacker _attack;

        [SerializeField] private UnityEvent OnNumberEqualsZero;
        [SerializeField] private UnityEvent<int> OnNumberChange;
        [SerializeField] private UnityEvent OnDamageTaken;

        public Fraction Fraction => _fraction;

        public Attacker Attacker => _attack;

        public void Regenerate()
        {
            _number.Increase();
        }

        private void Start()
        {
            _number.OnNumberEqualsZero.AddListener(OnNumberEqualsZero.Invoke);
            _number.OnNumberChange.AddListener(OnNumberChange.Invoke);
            _attack.OnDamageTaken.AddListener(OnDamageTaken.Invoke);
        }
    }
}
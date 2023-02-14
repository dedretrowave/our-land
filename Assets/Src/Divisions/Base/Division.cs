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
        [SerializeField] private Movement.Movement _movement;
        [SerializeField] private DivisionNumber _number;
        [SerializeField] private Attacker _attack;

        [Header("Events")]
        public UnityEvent OnNumberEqualsZero;
        public UnityEvent<int> OnNumberChange;
        public UnityEvent OnDamageTaken;

        private Fraction _fraction;

        public Attacker Attacker => _attack;

        public Fraction Fraction => _fraction;

        public void Regenerate()
        {
            _number.Increase();
        }

        public void SetInitialParameters(Fraction fraction = Fraction.Neutral, int number = 0)
        {
            _fraction = fraction;
            _number.Increase(number);
        }

        private void Start()
        {
            _number.OnNumberEqualsZero.AddListener(OnNumberEqualsZero.Invoke);
            _number.OnNumberChange.AddListener(OnNumberChange.Invoke);
            _attack.OnDamageTaken.AddListener(OnDamageTaken.Invoke);
        }
    }
}
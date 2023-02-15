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

        public Attacker Attacker => _attacker;

        public Fraction Fraction => _owner.Fraction;

        public void Regenerate()
        {
            _number.Increase();
        }

        public void SetInitialParameters(Fraction fraction = Fraction.Neutral, int number = 0)
        {
            _owner.ChangeFraction(fraction);
            _number.Increase(number);
        }

        private void Start()
        {
            _number.OnNumberEqualsZero.AddListener(OnNumberEqualsZero.Invoke);
            _number.OnNumberChange.AddListener(OnNumberChange.Invoke);
            _attacker.OnDamageTaken.AddListener(OnDamageTaken.Invoke);
        }
    }
}
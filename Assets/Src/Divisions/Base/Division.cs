using Src.Divisions.Number;
using Src.Regions;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Divisions.Base
{
    public abstract class Division : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Fraction _fraction;
        [SerializeField] private Movement.Movement _movement;
        [SerializeField] private DivisionNumber _number;
        [SerializeField] private Attack.Base.Attacker _attack;

        public UnityEvent OnNumberEqualsZero;
        public Fraction Fraction => _fraction;

        public void Regenerate()
        {
            _number.Increase();
        }

        public void TakeDamage()
        {
            _number.Decrease();
        }
    }
}
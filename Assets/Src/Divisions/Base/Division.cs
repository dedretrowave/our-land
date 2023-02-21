using System.Collections;
using Src.Helpers;
using Src.Interfaces;
using Src.Regions;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Divisions.Base
{
    public abstract class Division : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _gapBetweenAttacksInSeconds = 0.02f;
        
        [Header("Components")]
        [SerializeField] private Movement.Movement _movement;

        [Header("Events")]
        [SerializeField] private UnityEvent<int> _onNumberChange;

        public Fraction Fraction => _fraction;

        protected int number;
        protected ExecutionQueue queue;
        
        private Fraction _fraction;

        public void Initialize(int number, Fraction fraction)
        {
            _fraction = fraction;
            this.number = number;
            queue = gameObject.AddComponent<ExecutionQueue>();
        }

        public void Deploy(Region region)
        {
            _movement.ApplyPoint(region.transform.position);
        }

        public void TakeDamage()
        {
            int changedNumber = number - 1;

            if (changedNumber == 0)
            {
                number = 0;
                _onNumberChange.Invoke(number);
                Die();
                return;
            }
            
            number = changedNumber;
            _onNumberChange.Invoke(number);
        }

        public abstract void Attack(IDamageable target);
        
        protected IEnumerator AttackContinuously(IDamageable target)
        {
            if (target.Equals(null) || target.IsDead()) yield break;

            yield return new WaitForSeconds(_gapBetweenAttacksInSeconds);
            
            TakeDamage();
            target.TakeDamage();

            yield return AttackContinuously(target);
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
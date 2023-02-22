using System.Collections;
using Src.Helpers;
using Src.Interfaces;
using Src.Regions;
using Src.Regions.RegionDivisions.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Divisions.Base
{
    //TODO: Разргрузить ответственности?
    public abstract class Division : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] protected float GapBetweenAttacksInSeconds = 0.02f;
        
        [Header("Components")]
        [SerializeField] protected Movement.Movement _movement;

        [Header("Events")]
        [SerializeField] private UnityEvent<int> _onNumberChange;

        public Fraction Fraction => _fraction;
        
        protected ExecutionQueue queue;
        protected Health attackedRegionHealth;
        protected DivisionBase divisionBase;

        protected int Number
        {
            get => _number;
            set
            {
                if (value <= 0)
                {
                    _number = 0;
                    _onNumberChange.Invoke(_number);
                    Die();
                    return;
                }

                _number = value;
                _onNumberChange.Invoke(_number);
            }
        }

        private int _number;
        private Fraction _fraction;

        public void Initialize(int initialNumber, Fraction fraction, DivisionBase parentBase)
        {
            _fraction = fraction;
            _number = initialNumber;
            queue = gameObject.AddComponent<ExecutionQueue>();
            queue.OnExecutionFinished.AddListener(ProceedAfterSuccessfulAttack);
            divisionBase = parentBase;
        }

        public void AddAttackedRegionHealth(Health health)
        {
            attackedRegionHealth = health;
        }

        public void Deploy(Region region)
        {
            _movement.ApplyPoint(region.transform.position);
        }

        public void TakeDamage()
        {
            Number--;
        }

        protected abstract void ProceedAfterSuccessfulAttack();

        public void Attack(IDamageable target)
        {
            if (target.Equals(divisionBase)) return;
            
            queue.Add(AttackContinuously(target));
        }

        public void Supply(DivisionBase targetBase)
        {
            if (targetBase.Equals(divisionBase)) return;
            
            queue.Add(SupplyContinuously(targetBase));
        }
        
        protected abstract IEnumerator AttackContinuously(IDamageable target);

        private IEnumerator SupplyContinuously(DivisionBase targetBase)
        {
            if (targetBase.Equals(null) || targetBase.DivisionType != GetType()) yield break;

            targetBase.IncreaseNumber();
            Number--;

            yield return new WaitForSeconds(GapBetweenAttacksInSeconds);

            yield return SupplyContinuously(targetBase);
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using Src.Helpers;
using Src.Interfaces;
using Src.Regions;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Divisions.Combat.Base
{
    public abstract class Attacker : MonoBehaviour, IDamageable
    {
        [SerializeField] protected float pauseBetweenAttacks = .1f;
        [SerializeField] protected RegionOwner owner;
        
        [HideInInspector] public UnityEvent OnDamageTaken;

        protected ExecutionQueue _targetQueue;

        public void AttackTarget(IDamageable enemy)
        {
            _targetQueue.Add(Attack(enemy));
        }

        public void TakeDamage()
        {
            OnDamageTaken.Invoke();
        }

        public abstract void ProceedAfterEnemiesDefeated(Region region);

        protected abstract IEnumerator Attack(IDamageable enemy);
        
        private void Awake()
        {
            _targetQueue = gameObject.AddComponent<ExecutionQueue>();
        }
    }
}
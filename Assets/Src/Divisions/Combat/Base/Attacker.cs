using System.Collections;
using Src.Helpers;
using Src.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Divisions.Combat.Base
{
    public abstract class Attacker : MonoBehaviour, IDamageable
    {
        [HideInInspector] public UnityEvent OnDamageTaken;
        
        protected ExecutionQueue _targetQueue;

        public void AttackTarget(IDamageable enemy)
        {
            StartCoroutine(Attack(enemy));
            // _targetQueue.Add(Attack(enemy));
        }

        public void TakeDamage()
        {
            OnDamageTaken.Invoke();
        }

        protected abstract IEnumerator Attack(IDamageable enemy);
        
        private void Start()
        {
            _targetQueue = gameObject.AddComponent<ExecutionQueue>();
        }
    }
}
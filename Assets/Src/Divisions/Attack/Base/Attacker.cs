using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Divisions.Attack.Base
{
    public abstract class Attacker : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnDamageTaken;
        
        public void Attack(List<Attacker> attackers)
        {
            attackers.ForEach(Attack);
        }

        public void TakeDamage()
        {
            OnDamageTaken.Invoke();
        }

        public abstract void Attack(Attacker attacker);
    }
}
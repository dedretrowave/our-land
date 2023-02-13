using UnityEngine;
using UnityEngine.Events;

namespace Src.Divisions.Attack.Base
{
    public abstract class Attacker : MonoBehaviour
    {
        [HideInInspector] public UnityEvent OnDamageTaken;

        public void TakeDamage()
        {
            OnDamageTaken.Invoke();
        }

        public abstract void Attack(Attacker attacker);
    }
}
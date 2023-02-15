using System.Collections;
using Src.Divisions.Combat.Base;
using Src.Interfaces;
using Src.Regions;
using UnityEngine;

namespace Src.Divisions.Combat
{
    public class TanksAttacker : Attacker
    {
        public override void ProceedAfterEnemiesDefeated(Health health)
        {
            _targetQueue.Add(Attack(health));
        }

        protected override IEnumerator Attack(IDamageable enemy)
        {
            if (enemy.Equals(null) || enemy is PlanesAttacker)
            {
                yield break;
            }

            enemy.TakeDamage();

            yield return new WaitForSeconds(pauseBetweenAttacks);

            yield return Attack(enemy);
        }
    }
}
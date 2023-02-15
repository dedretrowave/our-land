using System.Collections;
using Src.Divisions.Combat.Base;
using Src.Interfaces;
using Src.Regions;
using UnityEngine;

namespace Src.Divisions.Combat
{
    public class PlanesAttacker : Attacker
    {
        public override void ProceedAfterEnemiesDefeated(Region region)
        {
            //CODE TO FLY AWAY
            Debug.Log($"{name} FLEW AWAY");
        }

        protected override IEnumerator Attack(IDamageable enemy)
        {
            if (enemy.Equals(null) || enemy is not Attacker)
            {
                Debug.Log($"{name} FINISH ATTACK");
                yield break;
            }

            enemy.TakeDamage();

            yield return new WaitForSeconds(pauseBetweenAttacks);

            yield return Attack(enemy);
        }
    }
}
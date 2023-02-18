using System.Collections;
using Src.Divisions.Combat.Base;
using Src.Interfaces;
using UnityEngine;

namespace Src.Divisions.Combat
{
    public class TanksAttacker : Attacker
    {

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
using System.Collections;
using Src.Divisions.Combat.Base;
using Src.Interfaces;

namespace Src.Divisions.Combat
{
    public class TanksAttacker : Attacker
    {
        protected override IEnumerator Attack(IDamageable enemy)
        {
            while (true)
            {
                if (enemy == null)
                {
                    yield return null;
                }
                
                Attack(enemy as TanksAttacker);
            }
        }

        private void Attack(TanksAttacker enemy)
        {
            if (enemy == null) return;
            
            enemy.TakeDamage();
        }
    }
}
using System.Collections;
using Src.Divisions.Combat.Base;
using Src.Interfaces;

namespace Src.Divisions.Combat
{
    public class PlanesAttacker : Attacker
    {
        protected override IEnumerator Attack(IDamageable enemy)
        {
            while (true)
            {
                if (enemy == null)
                {
                    yield return null;
                }
            
                enemy.TakeDamage();
            }
        }
    }
}
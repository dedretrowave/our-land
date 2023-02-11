using Src.Divisions.Attack.Base;

namespace Src.Divisions.Attack
{
    public class TanksAttacker : Attacker
    {
        public override void Attack(Attacker attacker)
        {
            Attack(attacker as TanksAttacker);
        }

        private void Attack(TanksAttacker enemy)
        {
            enemy.TakeDamage();
        }
    }
}
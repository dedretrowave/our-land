using Src.Divisions.Attack.Base;

namespace Src.Divisions.Attack
{
    public class PlanesAttacker : Attacker
    {
        public override void Attack(Attacker attacker)
        {
            attacker.TakeDamage();
        }
    }
}
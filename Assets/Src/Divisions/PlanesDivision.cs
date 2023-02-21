using Src.Divisions.Base;
using Src.Interfaces;
using Src.Regions;

namespace Src.Divisions
{
    public class PlanesDivision : Division
    {
        public override void Attack(IDamageable target)
        {
            if (target.Equals(null) || target is Health)
            {
                return;
            }
            
            queue.Add(AttackContinuously(target));
        }
    }
}
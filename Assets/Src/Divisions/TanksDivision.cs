using Src.Divisions.Base;
using Src.Interfaces;
using Src.Regions.RegionDivisions.Base;

namespace Src.Divisions
{
    public class TanksDivision : Division
    {
        public override void Attack(IDamageable target)
        {
            DivisionBase castedTarget = target as DivisionBase;

            if (target is null || (castedTarget != null && castedTarget.DivisionType == typeof(PlanesDivision)))
            {
                return;
            }
            
            queue.Add(AttackContinuously(target));
        }
    }
}
using System.Collections;
using Src.Divisions.Base;
using Src.Interfaces;
using Src.Regions.RegionDivisions.Base;
using UnityEngine;

namespace Src.Divisions
{
    public class TanksDivision : Division
    {
        protected override IEnumerator AttackContinuously(IDamageable target)
        {
            if (target.Equals(null) || target.IsDead()) yield break;

            yield return new WaitForSeconds(GapBetweenAttacksInSeconds);
            
            TakeDamage();

            DivisionBase castedTarget = target as DivisionBase;

            if (castedTarget != null && castedTarget.DivisionType == typeof(PlanesDivision))
            {
                yield break;
            }
            
            target.TakeDamage();

            yield return AttackContinuously(target);
        }
    }
}
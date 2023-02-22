using System.Collections;
using Src.Divisions.Base;
using Src.Interfaces;
using Src.Regions;
using UnityEngine;

namespace Src.Divisions
{
    public class PlanesDivision : Division
    {
        protected override void ProceedAfterSuccessfulAttack()
        {
            _movement.ApplyPoint(divisionBase.transform.position);
        }

        protected override IEnumerator AttackContinuously(IDamageable target)
        {
            if (target.Equals(null) || target.IsDead() || target is Health) yield break;

            yield return new WaitForSeconds(GapBetweenAttacksInSeconds);
            
            target.TakeDamage();
            TakeDamage();

            yield return AttackContinuously(target);
        }
    }
}
using System;
using System.Collections.Generic;
using Src.Divisions.Base;
using Src.Regions.RegionDivisions.Base;
using UnityEngine;

namespace Src.Regions.RegionCombat
{
    public class RegionDefence : MonoBehaviour
    {
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private Region _region;
        [SerializeField] private List<DivisionBase> _defenders;

        private Fraction _currentClaimerFraction;
        
        public void GiveUpRegion()
        {
            _region.ChangeOwner(_currentClaimerFraction);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Division enemy) && enemy.Fraction != _owner.Fraction)
            {
                Defend(enemy);
            }
        }

        private void Defend(Division offence)
        {
            SetRegionClaimer(offence.Fraction);
            
            _defenders.ForEach(offence.Attack);
        }

        private void SetRegionClaimer(Fraction fraction)
        {
            _currentClaimerFraction = fraction;
        }
    }
}
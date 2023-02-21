using System.Collections.Generic;
using Src.Divisions.Base;
using Src.Regions.RegionDivisions.Base;
using UnityEngine;

namespace Src.Regions.RegionCombat
{
    public class RegionDefence : MonoBehaviour
    {
        [SerializeField] private Region _region;
        [SerializeField] private Health _health;
        [SerializeField] private List<DivisionBase> _defenders;

        private Fraction _currentClaimerFraction;
        
        public void GiveUpRegion()
        {
            _region.ChangeOwner(_currentClaimerFraction);
        }

        private void Defend(Division offence)
        {
            SetRegionClaimer(offence.Fraction);
            
            _defenders.ForEach(offence.Attack);
            offence.Attack(_health);
        }

        private void SetRegionClaimer(Fraction fraction)
        {
            _currentClaimerFraction = fraction;
        }
    }
}
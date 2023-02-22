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
        [SerializeField] private Health _health;
        [SerializeField] private List<DivisionBase> _defenders;

        private readonly List<Division> _offenders = new();
        private Fraction _currentClaimerFraction;

        public void GiveUpRegion()
        {
            _region.ChangeOwner(_currentClaimerFraction);
            _offenders.ForEach(Supply);
            _offenders.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Division division)) return;

            if (division.Fraction != _owner.Fraction)
            {
                Defend(division);
            }
            else
            {
                Supply(division);
            }
        }

        private void Supply(Division division)
        {
            _defenders.ForEach(division.Supply);
        }

        private void Defend(Division offence)
        {
            _offenders.Add(offence);
            SetRegionClaimer(offence.Fraction);
            _defenders.ForEach(offence.Attack);
            offence.AddAttackedRegionHealth(_health);
        }

        private void SetRegionClaimer(Fraction fraction)
        {
            _currentClaimerFraction = fraction;
        }
    }
}
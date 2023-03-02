using System.Collections.Generic;
using Src.Divisions;
using Src.Regions.Fraction;
using Src.Regions.Structures;
using Src.Units.Defenders.Base;
using Src.Units.Divisions;
using UnityEngine;

namespace Src.Regions.Combat
{
    public class RegionCombatZone : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private DivisionBase _base;

        [Header("Defenders")]
        [SerializeField] private RegionDefence _defence;

        private Fraction.Fraction _regionClaimer;

        public void ChangeOwner()
        {
            _owner.Change(_regionClaimer);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Division division)) return;

            RegisterDivision(division);
        }

        private void RegisterDivision(Division division)
        {
            if (division.ParentBase.Equals(_base)) return;

            _regionClaimer = division.Fraction;

            if (division.Fraction != _owner.Fraction)
            {
                _defence.ApplyDefence(division);
                _base.TakeDamage(division.Number);
            }
            else
            {
                _base.TakeSupplies(division.Number);
            }
            
            Destroy(division.gameObject);
        }

        private void Awake()
        {
            _regionClaimer = _owner.Fraction;
        }
    }
}
using System.Collections.Generic;
using Src.Divisions;
using Src.Regions.Defence.Defenders.Base;
using Src.Regions.Fraction;
using Src.Regions.Structures;
using UnityEngine;

namespace Src.Regions.Defence
{
    public class RegionCombatZone : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private DivisionBase _base;

        [Header("Defenders")]
        [SerializeField] private List<Defender> _defenders;

        private Fraction.Fraction _regionClaimer;

        public void ChangeOwner()
        {
            _owner.Change(_regionClaimer);
        }

        private void RegisterDivision(Division division)
        {
            if (division.ParentBase.Equals(_base)) return;

            _regionClaimer = division.Fraction;

            if (division.Fraction != _owner.Fraction)
            {
                if (_defenders.Count > 0)
                {
                    _defenders.ForEach(defender => defender.InteractWithEnemy(division));
                }
                
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

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Division division)) return;

            RegisterDivision(division);
        }
    }
}
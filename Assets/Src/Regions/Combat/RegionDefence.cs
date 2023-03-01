using System;
using System.Collections.Generic;
using Src.Regions.Fraction;
using Src.Units.Defenders.Base;
using Src.Units.Divisions;
using UnityEngine;

namespace Src.Regions.Combat
{
    public class RegionDefence : MonoBehaviour
    {
        [SerializeField] private RegionOwner _owner; 
            
        private List<Defender> _defenders;

        public Fraction.Fraction Fraction => _owner.Fraction;

        public void ApplyDefence(Division enemy)
        {
            _defenders.ForEach(defender => defender.InteractWithEnemy(enemy));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Defender defender)) return;

            RegisterDefender(defender);
        }

        private void RegisterDefender(Defender defender)
        {
            _defenders.Add(defender);
        }
    }
}
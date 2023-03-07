using System;
using System.Collections.Generic;
using Src.Divisions.Defenders.Base;
using Src.Divisions.Divisions;
using Src.Regions.Fraction;
using UnityEngine;

namespace Src.Regions.Combat
{
    public class RegionDefence : MonoBehaviour
    {
        [SerializeField] private RegionOwner _owner; 
            
        private List<Defender> _defenders = new();

        public Fraction.Fraction Fraction => _owner.Fraction;

        public void ApplyDefence(Division enemy)
        {
            _defenders.ForEach(defender => defender.InteractWithEnemy(enemy));
        }

        public T GetDefenderOfType<T>()
        {
            return (T) GetDefenderOfType(typeof(T));
        }

        public void RegisterDefender(Defender defender)
        {
            _defenders.Add(defender);
        }
        
        private object GetDefenderOfType(Type type)
        {
            return _defenders.Find(defender => defender.GetType() == type);
        }
    }
}
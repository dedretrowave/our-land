using System;
using System.Collections.Generic;
using Src.Map.Garrisons.Defenders.Base;
using Src.Map.Garrisons.Divisions;
using UnityEngine;

namespace Src.Map.Regions.Combat
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
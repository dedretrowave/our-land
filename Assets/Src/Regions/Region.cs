using System;
using Src.Divisions.Base;
using UnityEngine;

namespace Src.Regions
{
    public class Region : MonoBehaviour
    {
        [SerializeField] private Fraction _fraction;
        [SerializeField] private RegionDefence.RegionCombatZone _combatZone;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Division>().Fraction != _fraction)
            {
                
            }
        }
    }
}
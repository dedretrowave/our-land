using Src.Regions;
using UnityEngine;

namespace Src.Divisions
{
    public class RegionOwner : MonoBehaviour
    {
        [SerializeField] private Fraction _fraction;

        public Fraction Fraction => _fraction;
    }
}
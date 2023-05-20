using Src.Map.Fraction;
using UnityEngine;

namespace Src.Map.Regions
{
    public class RegionOwner : MonoBehaviour
    {
        [SerializeField] private Character _fraction;

        public Character Fraction => _fraction;

        public void SetFraction(Character newOwner)
        {
            _fraction = newOwner;
        }
    }
}
using UnityEngine;

namespace Src.Regions
{
    public class RegionOwner : MonoBehaviour
    {
        [SerializeField] private Fraction.Fraction _fraction;

        public Fraction.Fraction Fraction => _fraction;

        public void SetFraction(Fraction.Fraction newOwner)
        {
            _fraction = newOwner;
        }
    }
}
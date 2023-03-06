using UnityEngine;
using UnityEngine.Events;

namespace Src.Regions.Fraction
{
    public class RegionOwner : MonoBehaviour
    {
        [SerializeField] private Fraction _fraction;

        public Fraction Fraction => _fraction;

        public void Change(Fraction newOwner)
        {
            _fraction = newOwner;
        }
    }
}
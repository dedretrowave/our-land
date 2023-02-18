using Src.Regions;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Divisions
{
    public class RegionOwner : MonoBehaviour
    {
        [SerializeField] private Fraction _fraction = Fraction.Neutral;

        public Fraction Fraction => _fraction;

        [SerializeField] private UnityEvent<Fraction> OnFractionChanged;

        public void ChangeFraction(Fraction fraction)
        {
            _fraction = fraction;
            OnFractionChanged.Invoke(_fraction);
        }
    }
}
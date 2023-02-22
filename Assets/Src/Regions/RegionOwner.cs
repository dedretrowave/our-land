using System;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Regions
{
    public class RegionOwner : MonoBehaviour
    {
        [SerializeField] private Fraction _fraction = Fraction.Neutral;

        public Fraction Fraction => _fraction;

        [SerializeField] private UnityEvent<Fraction> OnFractionChanged;

        private void Start()
        {
            OnFractionChanged.Invoke(_fraction);
        }

        public void ChangeFraction(Fraction fraction)
        {
            _fraction = fraction;
            OnFractionChanged.Invoke(_fraction);
        }
    }
}
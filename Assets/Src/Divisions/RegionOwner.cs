using Src.Regions;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Divisions
{
    public class RegionOwner : MonoBehaviour
    {
        [SerializeField] private Fraction _fraction;

        public Fraction Fraction => _fraction;

        public UnityEvent<Fraction> OnFractionChanged;

        public void ChangeOwner(Fraction fraction)
        {
            _fraction = fraction;
            OnFractionChanged.Invoke(_fraction);
        }
    }
}
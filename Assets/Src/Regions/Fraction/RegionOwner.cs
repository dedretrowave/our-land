using UnityEngine;
using UnityEngine.Events;

namespace Src.Regions.Fraction
{
    public class RegionOwner : MonoBehaviour
    {
        [SerializeField] private Fraction _fraction;

        [SerializeField] private UnityEvent<Fraction> _onFractionChange;

        public Fraction Fraction => _fraction;

        public void Change(Fraction newOwner)
        {
            _fraction = newOwner;
            _onFractionChange.Invoke(_fraction);
        }

        private void Start()
        {
            _onFractionChange.Invoke(_fraction);
        }
    }
}
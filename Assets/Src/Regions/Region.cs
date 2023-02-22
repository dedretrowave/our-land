using UnityEngine;

namespace Src.Regions
{
    public class Region : MonoBehaviour
    {
        [SerializeField] private RegionOwner _owner;

        private Fraction _newClaimerFraction;

        public void ChangeOwner(Fraction fraction)
        {
            _owner.ChangeFraction(fraction);
        }
    }
}
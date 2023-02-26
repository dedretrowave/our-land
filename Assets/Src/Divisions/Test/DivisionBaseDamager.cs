using Src.Regions;
using Src.Regions.Fraction;
using UnityEngine;

namespace Src.Divisions.Test
{
    public class DivisionBaseDamager : MonoBehaviour
    {
        [SerializeField] private RegionDefence _target;
        [SerializeField] private int _amount;
        [SerializeField] private Fraction _fraction = Fraction.Enemy;

        private void Start()
        {
            Division mock = gameObject.AddComponent<Division>();
            // mock.Init(_fraction, _amount);
            // _target.RegisterDivision(mock);
        }
    }
}
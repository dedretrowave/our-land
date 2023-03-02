using Src.Regions;
using Src.Regions.Combat;
using Src.Regions.Fraction;
using Src.Units.Divisions;
using UnityEngine;

namespace Src.Divisions.Test
{
    public class DivisionBaseDamager : MonoBehaviour
    {
        [SerializeField] private RegionCombatZone _target;
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
using System;
using Src.Divisions;
using Src.Regions.Fraction;
using UnityEngine;

namespace Src.Regions.Structures
{
    public class DivisionBase : MonoBehaviour
    {
        [SerializeField] private DivisionGarrison _garrison;
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private Division _divisionPrefab;

        private int _divisionsAfterAttack;

        public Division DeployDivision()
        {
            Division division = Instantiate(_divisionPrefab, transform.position, Quaternion.identity);
            division.Init(_owner.Fraction, _garrison.Amount, this);
            _garrison.DecreaseToNumber(0);
            return division;
        }

        public void TakeDamage(int amount)
        {
            if (_divisionsAfterAttack > 0)
            {
                _divisionsAfterAttack += amount;
                return;
            }

            int difference = _garrison.Amount - amount;

            if (difference < 0)
            {
                _divisionsAfterAttack = Math.Abs(difference);
                _garrison.DecreaseToNumber(0);
            }
            else
            {
                _garrison.DecreaseToNumber(difference);
            }
        }

        public void TakeSupplies(int amount)
        {
            int suppliedNumber = _garrison.Amount + amount;
            
            _garrison.IncreaseToNumber(suppliedNumber + _divisionsAfterAttack);
            _divisionsAfterAttack = 0;
        }
    }
}
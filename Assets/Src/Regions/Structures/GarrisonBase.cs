using System.Collections;
using Src.Divisions.Divisions;
using Src.Divisions.Garrison;
using Src.Global;
using Src.Regions.Fraction;
using Src.Settings;
using UnityEngine;

namespace Src.Regions.Structures
{
    public class GarrisonBase : MonoBehaviour
    {
        [SerializeField] private Garrison _garrison;
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private Division _divisionPrefab;

        private int _offence;
        private int _supply;

        private Coroutine _offenceRoutine;
        private Coroutine _supplyRoutine;

        private float _divisionChangeRate;

        public Division DeployDivision()
        {
            Division division = Instantiate(_divisionPrefab, transform.position, Quaternion.identity);
            division.Init(_owner.Fraction, _garrison.Amount, this);
            _garrison.DecreaseToZero();
            return division;
        }

        public void SwapOffenceAndSupply()
        {
            (_offence, _supply) = (_supply, _offence);
            
            RerunAttackRoutine();
            RerunSupplyRoutine();
        }

        public void AddOffence(int amount)
        {
            _offence += amount;
            
            RerunAttackRoutine();
        }

        public void AddSupply(int amount)
        {
            _supply += amount;
            
            RerunSupplyRoutine();
        }

        private void RerunAttackRoutine()
        {
            if (_offenceRoutine != null) StopCoroutine(_offenceRoutine);
            _offenceRoutine = StartCoroutine(TakeDamage());
        }

        private void RerunSupplyRoutine()
        {
            if (_supplyRoutine != null) StopCoroutine(_supplyRoutine);
            _supplyRoutine = StartCoroutine(TakeSupplies());
        }

        private IEnumerator TakeDamage()
        {
            if (_offence <= 0)
            {
                _offence = 0;
                yield break;
            }

            yield return new WaitForSeconds(_divisionChangeRate);
            
            _garrison.Decrease();
            _offence -= 1;

            yield return TakeDamage();
        }

        private IEnumerator TakeSupplies()
        {
            if (_supply <= 0)
            {
                _supply = 0;
                yield break;
            }

            yield return new WaitForSeconds(_divisionChangeRate);
            
            _garrison.Increase();
            _supply -= 1;

            yield return TakeSupplies();
        }

        private void Start()
        {
            _divisionChangeRate = DependencyContext.Dependencies.Get<Config>().DivisionChangeRateInSeconds;
        }
    }
}
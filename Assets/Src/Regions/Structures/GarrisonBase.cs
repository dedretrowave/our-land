using System.Collections;
using Src.DI;
using Src.Divisions.Divisions;
using Src.Divisions.Garrison;
using Src.Settings;
using UnityEngine;

namespace Src.Regions.Structures
{
    public class GarrisonBase : MonoBehaviour
    {
        [SerializeField] private Garrison _garrison;
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private Division _divisionPrefab;

        private Coroutine _offenceRoutine;
        private Coroutine _supplyRoutine;

        private float _divisionSpawnRate;

        public void DeployDivisions(Vector3 targetPoint)
        {
            StartCoroutine(SpawnDivisionsOneByOne(targetPoint));
        }

        public void TakeDamage()
        {
            _garrison.Decrease();
        }

        public void TakeSupply()
        {
            _garrison.Increase();
        }

        private void Start()
        {
            _divisionSpawnRate = DependencyContext.Dependencies.Get<Config>().DivisionChangeRateInSeconds;
        }

        private IEnumerator SpawnDivisionsOneByOne(Vector3 targetPoint)
        {
            int i = 0;
            int startGarrisonAmount = _garrison.Amount;
            
            while (i < startGarrisonAmount)
            {
                // Division division = Instantiate(_divisionPrefab, transform.position, Quaternion.identity);
                Division division = Instantiate(_divisionPrefab, transform);
                division.Init(_owner.Fraction, this);
                division.Deploy(targetPoint);
                _garrison.Decrease();
                i++;

                yield return new WaitForSeconds(_divisionSpawnRate);
            }
        }
    }
}
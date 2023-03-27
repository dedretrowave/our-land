using System.Collections;
using Src.DI;
using Src.Garrisons;
using Src.Garrisons.Divisions;
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

        public void DeployDivisions(Region targetRegion)
        {
            StartCoroutine(SpawnDivisionsOneByOne(targetRegion));
        }

        public void TakeDamage()
        {
            _garrison.TakeDamage();
        }

        public void TakeSupply()
        {
            _garrison.Increase();
        }

        private void Start()
        {
            _divisionSpawnRate = DependencyContext.Dependencies.Get<Config>().DivisionChangeRateInSeconds;
        }

        private IEnumerator SpawnDivisionsOneByOne(Region targetRegion)
        {
            int i = 0;
            int startGarrisonAmount = _garrison.Amount;
            
            while (i < startGarrisonAmount)
            {
                Division division = Instantiate(_divisionPrefab, transform);
                division.Init(_owner.Fraction, this, targetRegion);
                division.Deploy(targetRegion.GetPosition());
                _garrison.Decrease();
                i++;

                yield return new WaitForSeconds(_divisionSpawnRate);
            }
        }
    }
}
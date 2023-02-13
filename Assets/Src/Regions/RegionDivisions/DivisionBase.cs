using System.Collections;
using Src.Divisions;
using Src.Divisions.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Regions.RegionDivisions
{
    public class DivisionBase : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private Division _divisionPrefab;
        [Header("Parameters")]
        [SerializeField] private float _increaseTimeSpan;
        [SerializeField] private float _freezeTimeSpan;

        [SerializeField] private UnityEvent<Division> OnNewDivisionSpawned;

        private Coroutine _increaseRoutine;
        private Division _division;

        public Division ReleaseDivision()
        {
            Division thisDivision = _division;
            _division = null;
            CreateNewDivision();
            return thisDivision;
        }

        private void Start()
        {
            CreateNewDivision();
        }

        private void CreateNewDivision()
        {
            _division = Instantiate(_divisionPrefab, transform);
            _division.Fraction = _owner.Fraction;
            StartIncreasing();
            OnNewDivisionSpawned.Invoke(_division);
        }

        private void StartIncreasing()
        {
            _increaseRoutine = StartCoroutine(IncreaseContinuously());
        }
        
        public void DestroyDivision()
        {
            StopCoroutine(_increaseRoutine);

            StartCoroutine(RespawnAfterCooldown());
        }

        private IEnumerator RespawnAfterCooldown()
        {
            yield return new WaitForSeconds(_freezeTimeSpan);
            
            CreateNewDivision();
        }

        private IEnumerator IncreaseContinuously()
        {
            yield return new WaitForSeconds(_increaseTimeSpan);
            
            _division.Regenerate();

            yield return IncreaseContinuously();
        }
    }
}
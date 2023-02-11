using System.Collections;
using Src.Divisions.Base;
using Src.Divisions.Number;
using UnityEngine;

namespace Src.Regions.RegionDivisions
{
    public class DivisionBase : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Division _divisionPrefab;
        [Header("Parameters")]
        [SerializeField] private float _increaseTimeSpan;
        [SerializeField] private float _freezeTimeSpan;

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
            _division.OnNumberEqualsZero.AddListener(DestroyDivision);
        }

        private void CreateNewDivision()
        {
            _division = Instantiate(_divisionPrefab, transform);
            StartIncreasing();
        }

        private void StartIncreasing()
        {
            _increaseRoutine = StartCoroutine(IncreaseContinuously());
        }
        
        private void DestroyDivision()
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
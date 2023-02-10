using System.Collections;
using Src.Divisions.Base;
using UnityEngine;

namespace Src.Regions
{
    public class DivisionIncreaser : MonoBehaviour
    {
        [SerializeField] private Division _division;
        [SerializeField] private float _spawnRate;

        private Coroutine _divisionsCoroutine;

        private void Start()
        {
            Begin();
        }

        public void Pause()
        {
            StopCoroutine(_divisionsCoroutine);
        }

        public void Begin()
        {
            _divisionsCoroutine = StartCoroutine(IncreaseAfterTimeout(_division, _spawnRate));
        }

        private IEnumerator IncreaseAfterTimeout(Division division, float spawnRate)
        {
            yield return new WaitForSeconds(spawnRate);
            division.Increase();

            yield return IncreaseAfterTimeout(division, spawnRate);
        }
    }
}
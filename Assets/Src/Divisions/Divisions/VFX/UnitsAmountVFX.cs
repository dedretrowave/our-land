using System.Collections;
using UnityEngine;

namespace Src.Divisions.Divisions.VFX
{
    public class UnitsAmountVFX : MonoBehaviour
    {
        [SerializeField] private GameObject _tankMesh;

        [SerializeField] private float _spawnRate = .5f;
        private float _offset = 2f;

        public void Init(Division division)
        {
            StartCoroutine(SpawnOneByOneWithTimeout(division.Amount));
        }

        private IEnumerator SpawnOneByOneWithTimeout(int amount)
        {
            int i = 0;
            while (amount > 0)
            {
                for (int j = 0; j < Random.Range(1, 4); j++)
                {
                    Transform newTank = Instantiate(_tankMesh, transform).transform;
                    newTank.localPosition = _offset * new Vector3(Random.Range(-1f, 1f), Random.Range(-2f, 2f), i);
                }
                i++;
                amount--;
            
                yield return new WaitForSeconds(_spawnRate);
            }
        }
    }
}
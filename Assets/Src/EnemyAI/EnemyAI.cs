using System.Collections;
using System.Collections.Generic;
using Src.Regions;
using Src.Regions.Containers;
using UnityEngine;

namespace Src.EnemyAI
{
    public class EnemyAI : MonoBehaviour
    {
        [Header("Containers")]
        [SerializeField] private List<RegionContainer> _enemyContainers;
        [SerializeField] private RegionContainer _container;

        [Header("Parameters")]
        [SerializeField] private float _minPauseBetweenAttacks = 5f;
        [SerializeField] private float _maxPauseBetweenAttacks = 10f;

        private Region _selectedEnemyRegion;
        private float _waitTimeBeforeNextAttack;

        private void Start()
        {
            PrepareForAttack();
        }

        private void PrepareForAttack()
        {
            _waitTimeBeforeNextAttack = Random.Range(_minPauseBetweenAttacks, _maxPauseBetweenAttacks);

            if (_enemyContainers.Count == 0) return;
            
            _selectedEnemyRegion = _enemyContainers[Random.Range(0, _enemyContainers.Count)].GetRandomRegion();

            StartCoroutine(WaitAndAttack());
        }

        private IEnumerator WaitAndAttack()
        {
            yield return new WaitForSeconds(_waitTimeBeforeNextAttack);
            
            Attack();
        }

        private void Attack()
        {
            Region selectedRegion = _container.GetRandomRegion();
            
            selectedRegion.Base.DeployDivisions(_selectedEnemyRegion.transform.position);
            
            _selectedEnemyRegion.OnOwnerChange.AddListener(UnbindSelectedRegionAndPrepareForAttack);
        }

        private void UnbindSelectedRegionAndPrepareForAttack(Fraction.Fraction _)
        {
            _selectedEnemyRegion.OnOwnerChange.RemoveListener(UnbindSelectedRegionAndPrepareForAttack);
            _selectedEnemyRegion = null;
            
            PrepareForAttack();
        }
    }
}
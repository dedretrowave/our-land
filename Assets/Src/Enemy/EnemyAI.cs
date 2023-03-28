using System.Collections;
using System.Collections.Generic;
using Src.Map.Regions;
using Src.Map.Regions.Containers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Src.Enemy
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

            RegionContainer enemyContainer = _enemyContainers[Random.Range(0, _enemyContainers.Count)];

            if (enemyContainer == null)
            {
                StartCoroutine(WaitAndAttack());
                return;
            }

            _selectedEnemyRegion = enemyContainer.GetRandomRegion();
            StartCoroutine(WaitAndAttack());
        }

        private IEnumerator WaitAndAttack()
        {
            yield return new WaitForSeconds(_waitTimeBeforeNextAttack);
            
            Attack();
        }

        private void Attack()
        {
            if (_selectedEnemyRegion == null)
            {
                PrepareForAttack();
                return;
            }
            
            Region selectedRegion = _container.GetRandomRegion();

            selectedRegion.Base.DeployDivisions(_selectedEnemyRegion);

            _selectedEnemyRegion = null;
            PrepareForAttack();
        }
    }
}
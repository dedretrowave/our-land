using System;
using System.Collections;
using System.Collections.Generic;
using Src.Levels.Level;
using Src.Map.Fraction;
using Src.Map.Regions;
using Src.Map.Regions.Containers;
using Src.SerializableDictionary.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Src.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        private Dictionary<Map.Fraction.Fraction, RegionContainer> _containers = new();
        
        [Header("Containers")]
        [SerializeField] private List<RegionContainer> _enemyContainers;
        [SerializeField] private RegionContainer _container;

        [Header("Parameters")]
        [SerializeField] private float _minPauseBetweenAttacks = 5f;
        [SerializeField] private float _maxPauseBetweenAttacks = 10f;

        public UnityEvent OnGiveUp;

        private Region _selectedEnemyRegion;
        private float _waitTimeBeforeNextAttack;

        private Coroutine _routine;

        public void InitializeWithLevel(Levels.Level.Level level)
        {
            _container = _containers[level.Owner];
            _container.OnEmpty.AddListener(GiveUp);
            PrepareForAttack();
        }

        public void Disable()
        {
            StopCoroutine(_routine);
        }

        private void GiveUp()
        {
            _container.Clear();
            OnGiveUp.Invoke();
        }

        private void Awake()
        {
            RegionContainer[] childContainers = GetComponentsInChildren<RegionContainer>();

            foreach (RegionContainer childContainer in childContainers)
            {
                _containers.Add(childContainer.Owner.Fraction, childContainer);
            }
        }

        private void PrepareForAttack()
        {
            _waitTimeBeforeNextAttack = Random.Range(_minPauseBetweenAttacks, _maxPauseBetweenAttacks);

            RegionContainer enemyContainer = _enemyContainers[Random.Range(0, _enemyContainers.Count)];

            if (enemyContainer == null)
            {
                _routine = StartCoroutine(WaitAndAttack());
                return;
            }

            _selectedEnemyRegion = enemyContainer.GetRandomRegion();
            _routine = StartCoroutine(WaitAndAttack());
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
    
    
    [Serializable] 
    internal class FractionToContainer : SerializableDictionary<Character, RegionContainer> {}
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(FractionToContainer))]
    internal class FractionToContainerUI : SerializableDictionaryPropertyDrawer {}
#endif
}
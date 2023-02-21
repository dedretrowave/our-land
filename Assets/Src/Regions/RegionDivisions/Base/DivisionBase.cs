using System;
using System.Collections;
using Src.Divisions.Base;
using Src.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Regions.RegionDivisions.Base
{
    public class DivisionBase : MonoBehaviour, IDamageable
    {
        [Header("Components")]
        [SerializeField] private RegionOwner _owner;
        [SerializeField] protected Division _divisionPrefab;
        [Header("Parameters")]
        [SerializeField] private float _increaseTimeSpan;
        [SerializeField] private int _initialNumber;

        [SerializeField] private UnityEvent<int> _onNumberChange;
        
        private Coroutine _increaseRoutine;
        private Coroutine _spawnRoutine;
        private int _number;
        
        private int Number
        {
            get => _number;
            set
            {
                if (value <= 0)
                {
                    _number = 0;
                    _onNumberChange.Invoke(_number);
                    return;
                }
            
                _number = value;
                _onNumberChange.Invoke(_number);
            }
        }

        public Type DivisionType => _divisionPrefab.GetType();

        public void SendDivision(Region region)
        {
            Division division = Instantiate(_divisionPrefab, transform.position, Quaternion.identity);
            
            division.Initialize(Number, _owner.Fraction);
            Number = 0;
            division.Deploy(region);
        }
        
        public void TakeDamage()
        {
            Number -= 1;
        }

        public bool IsDead()
        {
            return Number == 0;
        }

        private void Start()
        {
            Number = _initialNumber;
            StartIncreasing();
        }

        private void StartIncreasing()
        {
            _increaseRoutine = StartCoroutine(IncreaseContinuously());
        }

        private IEnumerator IncreaseContinuously()
        {
            yield return new WaitForSeconds(_increaseTimeSpan);

            Number++;
            _onNumberChange.Invoke(Number);

            yield return IncreaseContinuously();
        }
    }
}
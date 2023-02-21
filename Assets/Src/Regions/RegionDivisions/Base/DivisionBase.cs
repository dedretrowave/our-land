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
        [SerializeField] private int _freezeTimeout;

        [SerializeField] private UnityEvent<int> _onNumberChange;
        [SerializeField] private UnityEvent _onNumberIncreaseFreeze;
        [SerializeField] private UnityEvent _onNumberIncreaseUnFreeze;

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
                    _onNumberIncreaseFreeze.Invoke();
                    StartCoroutine(Freeze());
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

        private IEnumerator Freeze()
        {
            StopIncrease();
            
            yield return new WaitForSeconds(_freezeTimeout);
            
            _onNumberIncreaseUnFreeze.Invoke();
            
            StartIncreasing();
        }

        private void StartIncreasing()
        {
            _spawnRoutine = StartCoroutine(IncreaseContinuously());
        }
        
        private void StopIncrease()
        {
            StopCoroutine(_spawnRoutine);
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
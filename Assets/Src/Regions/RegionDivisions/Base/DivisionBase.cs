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

        private int _number;
        private Coroutine _increaseRoutine;
        private Coroutine _spawnRoutine;

        public Type DivisionType => _divisionPrefab.GetType();

        public void SendDivision(Region region)
        {
            Division division = Instantiate(_divisionPrefab);
            
            division.Initialize(_number, _owner.Fraction);
            division.Deploy(region);
        }

        private void Start()
        {
            _number = _initialNumber;
            StartIncreasing();
        }

        private void StartIncreasing()
        {
            _increaseRoutine = StartCoroutine(IncreaseContinuously());
        }

        private IEnumerator IncreaseContinuously()
        {
            yield return new WaitForSeconds(_increaseTimeSpan);

            _number++;

            yield return IncreaseContinuously();
        }

        public void TakeDamage()
        {
            int changedNumber = _number - 1;

            if (changedNumber == 0)
            {
                _number = 0;
                _onNumberChange.Invoke(_number);
                return;
            }
            
            _number = changedNumber;
            _onNumberChange.Invoke(_number);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using EventBus;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    public class RandomRegionCapture : MonoBehaviour
    {
        [SerializeField] private int _timeout = 200;

        private EventBus.EventBus _eventBus;
        
        private List<MapRegionInstaller> _regions;

        private Coroutine _routine;

        private void Start()
        {
            _eventBus = EventBus.EventBus.Instance;
            _regions = new(GetComponentsInChildren<MapRegionInstaller>());
            
            StartRoutine();
            
            _eventBus.AddListener(EventName.ON_LEVEL_STARTED, StopRoutine);
            _eventBus.AddListener(EventName.ON_LEVEL_ENDED, StartRoutine);
        }

        private void OnDisable()
        {
            _eventBus.RemoveListener(EventName.ON_LEVEL_STARTED, StopRoutine);
            _eventBus.RemoveListener(EventName.ON_LEVEL_ENDED, StartRoutine);
        }

        private void StopRoutine()
        {
            StopCoroutine(_routine);
        }

        private void StartRoutine()
        {
            _routine = StartCoroutine(CaptureAfterTimeout());
        }

        private IEnumerator CaptureAfterTimeout()
        {
            yield return new WaitForSeconds(_timeout);

            int selectedRegionNumber = Random.Range(0, _regions.Count);

            MapRegionInstaller selectedRegion = _regions[selectedRegionNumber];

            if (selectedRegion.CurrentOwner.Fraction == Fraction.Fraction.Enemy)
            {
                yield return CaptureAfterTimeout();
            }
            
            selectedRegion.SetRandomEnemyOwner();

            yield return CaptureAfterTimeout();
        }
    }
}
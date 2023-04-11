using System;
using System.Collections;
using System.Collections.Generic;
using Src.Levels;
using Src.Levels.Level;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Src.Enemy
{
    public class RandomLevelCapture : MonoBehaviour
    {
        [SerializeField] private LevelContainer _container;
        
        [Header("Parameter")]
        [SerializeField] private float _chanceOfRandomCaptureInPercent = 45f;
        [SerializeField] private float _timeGapBetweenCaptureProc = 60f;

        private List<Level> _levels;
        private Coroutine _captureRoutine;

        public void RandomlyCaptureByPassingJustCompleteLevel(Level bypassLevel)
        {
            SortCompleteLevels();

            _levels.Remove(bypassLevel);
            
            RandomlyCapture();
        }

        private void RandomlyCapture()
        {
            float randomNumber = Random.Range(0f, 1f);

            if (randomNumber > _chanceOfRandomCaptureInPercent / 100f) return;

            if (_levels.Count == 0)
            { 
                LaunchRoutine();
                return;
            }

            Level selectedLevel = _levels[Random.Range(0, _levels.Count)];

            selectedLevel.SetRandomOwnerBesidesPlayer();
            
            LaunchRoutine();
        }

        private void Start()
        {
            SortCompleteLevels();
            LaunchRoutine();
        }
        
        private void LaunchRoutine()
        {
            if (_captureRoutine != null) StopCoroutine(_captureRoutine);
            
            _captureRoutine = StartCoroutine(RandomCaptureAfterTimeout());
        }

        private IEnumerator RandomCaptureAfterTimeout()
        {
            yield return new WaitForSeconds(_timeGapBetweenCaptureProc);
            
            RandomlyCapture();
            
            LaunchRoutine();
        }

        private void SortCompleteLevels()
        {
            _levels = _container.Levels;
            
            List<Level> sortedLevels = new();
            
            _levels.ForEach(level =>
            {
                if (level.IsControlledByPlayer)
                {
                    sortedLevels.Add(level);
                }
            });

            _levels = new(sortedLevels);
        }
    }
}
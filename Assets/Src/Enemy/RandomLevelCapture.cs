using System;
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

        private List<Level> _levels;

        public void RandomlyCapture(Level bypassLevel)
        {
            SortLevelsByCompletion();

            _levels.Remove(bypassLevel);

            int randomNumber = Random.Range(0, 2);

            if (randomNumber != 1) return;

            Level selectedLevel = _levels[Random.Range(0, _levels.Count)];
            
            selectedLevel.ChangeStatusToIncomplete();
        }
            
        private void Start()
        {
            SortLevelsByCompletion();
        }

        private void SortLevelsByCompletion()
        {
            _levels = _container.Levels;
            
            List<Level> sortedLevels = new();
            
            _levels.ForEach(level =>
            {
                if (level.Status == LevelCompletionState.Complete)
                {
                    sortedLevels.Add(level);
                }
            });

            _levels = new(sortedLevels);
        }
    }
}
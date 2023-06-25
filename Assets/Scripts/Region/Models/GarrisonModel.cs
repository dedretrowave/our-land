using System;

namespace Region.Models
{
    public class GarrisonModel
    {
        private int _count;
        private float _divisionSpawnRate;
        private float _increaseRate;
        
        public int Count => _count;
        public float DivisionSpawnRate => _divisionSpawnRate;
        public float IncreaseRate => _increaseRate;
        
        public GarrisonModel(int initialCount, float increaseRate, float divisionSpawnRate)
        {
            _count = initialCount;
            _increaseRate = increaseRate;
            _divisionSpawnRate = divisionSpawnRate;
        }
        
        public void ChangeCount(int count = 1)
        {
            int newCount = _count + count;
            
            if (newCount < 0)
            {
                _count = 0;
                throw new Exception("Garrison is zero");
            }

            _count = newCount;
        }
    }
}
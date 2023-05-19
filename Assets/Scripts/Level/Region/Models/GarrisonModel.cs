using System;

namespace Level.Region.Models
{
    public class GarrisonModel
    {
        private int _count;
        private float _increaseRate;

        public int Count => _count;
        public float IncreaseRate => _increaseRate;

        public GarrisonModel(int initialCount, float increaseRate)
        {
            _count = initialCount;
            _increaseRate = increaseRate;
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
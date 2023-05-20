using System;
using Characters.Base;

namespace Level.Region.Models
{
    public class RegionModel
    {
        private int _count;
        private float _increaseRate;
        private Character _currentOwner;

        public int Count => _count;
        public float IncreaseRate => _increaseRate;
        public Character CurrentOwner => CurrentOwner;

        public RegionModel(int initialCount, float increaseRate)
        {
            _count = initialCount;
            _increaseRate = increaseRate;
        }

        public void SetOwner(Character newOwner)
        {
            _currentOwner = newOwner;
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
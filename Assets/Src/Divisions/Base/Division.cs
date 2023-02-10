using Src.Regions;
using UnityEngine;

namespace Src.Divisions.Base
{
    public abstract class Division : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] protected float IncreaseSpeed = 1f;

        [Header("Components")]
        [SerializeField] protected Movement.Movement Movement;

        protected int Number;
        protected Region TargetRegion;

        public void SetBaseDivisions(int number)
        {
            Number = number;
        }

        public void Increase()
        {
            Number += 1;
        }

        public void Decrease()
        {
            Number -= 1;
        }

        public void Move(Transform point)
        {
            Movement.ApplyPoint(point);
        }
        
        protected abstract void Attack(Region region);
    }
}
using UnityEngine;

namespace Src.Regions.Divisions.Base
{
    public abstract class Division : MonoBehaviour
    {
        protected int Number;

        public void Increase()
        {
            Number += 1;
        }

        public void Decrease()
        {
            Number -= 1;
        }

        public abstract void Attack(Region region);
    }
}
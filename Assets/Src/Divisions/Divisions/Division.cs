using Src.Regions.Fraction;
using Src.Regions.Structures;
using UnityEngine;

namespace Src.Divisions.Divisions
{
    public class Division : MonoBehaviour
    {
        [SerializeField] private Movement.Movement _movement;
        
        private Fraction _fraction;
        private int _amount;
        private GarrisonBase _parentBase;

        public int Amount => _amount;

        public Fraction Fraction => _fraction;
        public GarrisonBase ParentBase => _parentBase;

        public void Init(Fraction fraction, int number, GarrisonBase parentBase)
        {
            _fraction = fraction;
            _amount = number;
            _parentBase = parentBase;
        }

        public void Deploy(Vector3 point)
        {
            _movement.ApplyPoint(point);
        }
    }
}
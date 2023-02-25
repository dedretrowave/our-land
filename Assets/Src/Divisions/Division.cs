using Src.Divisions.Movement;
using Src.Regions.Fraction;
using UnityEngine;

namespace Src.Divisions
{
    public class Division : MonoBehaviour
    {
        [SerializeField] private DivisionMovement _movement;
        
        private Fraction _fraction;
        private int _number;

        public int Number => _number;

        public Fraction Fraction => _fraction;

        public void Init(Fraction fraction, int number)
        {
            _fraction = fraction;
            _number = number;
        }

        public void Deploy(Vector3 point)
        {
            _movement.ApplyPoint(point);
        }
    }
}
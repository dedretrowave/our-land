using Src.Regions.Fraction;
using Src.Regions.Structures;
using UnityEngine;

namespace Src.Units.Divisions
{
    public class Division : MonoBehaviour
    {
        [SerializeField] private Units.Movement.Movement _movement;
        
        private Fraction _fraction;
        private int _number;
        private DivisionBase _parentBase;

        public int Number => _number;

        public Fraction Fraction => _fraction;
        public DivisionBase ParentBase => _parentBase;

        public void Init(Fraction fraction, int number, DivisionBase parentBase)
        {
            _fraction = fraction;
            _number = number;
            _parentBase = parentBase;
        }

        public void Deploy(Vector3 point)
        {
            _movement.ApplyPoint(point);
        }
    }
}
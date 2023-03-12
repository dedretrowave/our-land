using Src.Regions.Structures;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Divisions.Divisions
{
    public class Division : MonoBehaviour
    {
        [SerializeField] private Movement.Movement _movement;
        [SerializeField] private UnityEvent<Division> _onInit;

        private Fraction.Fraction _fraction;
        private int _amount;
        private GarrisonBase _parentBase;

        public int Amount => _amount;

        public Fraction.Fraction Fraction => _fraction;
        public GarrisonBase ParentBase => _parentBase;

        public void Init(Fraction.Fraction fraction, int number, GarrisonBase parentBase)
        {
            _fraction = fraction;
            _amount = number;
            _parentBase = parentBase;
            _onInit.Invoke(this);
        }

        public void Deploy(Vector3 point)
        {
            _movement.ApplyPoint(point);
        }
    }
}
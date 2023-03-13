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
        private GarrisonBase _parentBase;

        public Fraction.Fraction Fraction => _fraction;
        public GarrisonBase ParentBase => _parentBase;

        public void Init(Fraction.Fraction fraction, GarrisonBase parentBase)
        {
            _fraction = fraction;
            _parentBase = parentBase;
            _onInit.Invoke(this);
            Vector3 transformLocalPosition = transform.localPosition;
            transformLocalPosition.x += Random.Range(-20f, 20f);
            transform.localPosition = transformLocalPosition;
        }

        public void Deploy(Vector3 point)
        {
            _movement.ApplyPoint(point);
        }
    }
}
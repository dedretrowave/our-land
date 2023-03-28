using Src.Map.Regions;
using Src.Map.Regions.Structures;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Map.Garrisons.Divisions
{
    public class Division : MonoBehaviour
    {
        [SerializeField] private Movement.Movement _movement;
        [SerializeField] private UnityEvent<Division> _onInit;

        private Fraction.Fraction _fraction;
        private GarrisonBase _parentBase;
        private Region _targetRegion;

        public Fraction.Fraction Fraction => _fraction;
        public GarrisonBase ParentBase => _parentBase;
        public Region TargetRegion => _targetRegion;

        public void Init(Fraction.Fraction fraction, GarrisonBase parentBase, Region targetRegion)
        {
            _fraction = fraction;
            _parentBase = parentBase;
            _targetRegion = targetRegion;
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
using Src.Interfaces;
using UnityEngine;

namespace Src.Regions
{
    public class MovementTarget : MonoBehaviour, IMovementTargetable
    {
        [SerializeField] private Transform _movementPoint;

        public Vector3 GetPosition()
        {
            return _movementPoint.position;
        }
    }
}
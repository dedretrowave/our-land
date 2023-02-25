using UnityEngine;

namespace Src.Divisions.Movement
{
    public class DivisionMovement : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _speed;
        [SerializeField] private float _stopDistance = 0.1f;
        
        private Vector3 _point;

        public void ApplyPoint(Vector3 point)
        {
            _point = point;
            transform.LookAt(_point);
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _point) >= _stopDistance)
            {
                MoveTowards();
            }
        }
        
        private void MoveTowards()
        {
            transform.position = Vector3.MoveTowards(transform.position, _point, _speed * Time.deltaTime);
        }
    }
}
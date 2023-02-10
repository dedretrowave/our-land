using UnityEngine;

namespace Src.Divisions.Movement
{
    public class Movement : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _speed;
        [SerializeField] private float _stopDistance;
        
        private Transform _point;

        public void ApplyPoint(Transform point)
        {
            _point = point;
            transform.LookAt(_point);
        }

        private void MoveTowards()
        {
            transform.position = Vector3.MoveTowards(transform.position, _point.position, _speed * Time.deltaTime);
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _point.position) >= _stopDistance)
            {
                MoveTowards();
            }
        }
    }
}
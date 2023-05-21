using System;
using Characters.Base;
using Level.Region.Views;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Components
{
    public class Division : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _speed;
        [SerializeField] private float _stopDistance = 0.1f;
        
        private Character _character;
        private RegionView _target;

        public Character Owner => _character;

        public void Construct(Character owner, RegionView target)
        {
            _character = owner;
            _target = target;

            Vector3 transformLocalPosition = transform.localPosition;
            transformLocalPosition.x += Random.Range(-20f, 20f);
            transform.localPosition = transformLocalPosition;
            transform.LookAt(_target.transform, Vector3.back);
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _target.transform.position) >= _stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, 
                    _speed * Time.deltaTime);
            }
        }
    }
}
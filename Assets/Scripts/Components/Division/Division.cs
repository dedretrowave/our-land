using Characters.Base;
using Characters.Skins;
using Components.Division.UI;
using Region.Views;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Components.Division
{
    public class Division : MonoBehaviour
    {
        private DivisionView _view;
        
        [Header("Parameters")]
        [SerializeField] private float _speed;
        [SerializeField] private float _stopDistance = 0.1f;
        
        private Character _character;
        private GarrisonView _target;

        public Character Owner => _character;
        public GarrisonView Target => _target;

        public void Construct(Character owner, GarrisonView target)
        {
            _view = GetComponent<DivisionView>();
            _character = owner;
            _target = target;
            
            _view.SetSkin(owner.Skin);

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
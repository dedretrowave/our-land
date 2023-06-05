using System;
using Characters.Model;
using Components.Division;
using TMPro;
using UnityEngine;

namespace Region.Views
{
    public class GarrisonView : MonoBehaviour
    {
        [Header("Components")]
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI _countText;
        
        [Header("Prefabs")]
        [SerializeField] private Division _division;

        [Header("Parameters")]
        [SerializeField] private float _garrisonIncreaseRate;
        [SerializeField] private int _garrisonInitialCount;
        [SerializeField] private float _divisionSpawnRate;

        public float DivisionSpawnRate => _divisionSpawnRate;
        public float GarrisonIncreaseRate => _garrisonIncreaseRate;
        public int GarrisonInitialCount => _garrisonInitialCount;

        public event Action<Division> OnDamageTaken;
        public event Action<Transform> OnGarrisonRelease;

        public void SetCount(int count)
        {
            _countText.text = count.ToString();
        }

        public void Release(Transform point)
        {
            OnGarrisonRelease?.Invoke(point);
        }

        public void SendDivision(GarrisonView target, CharacterModel owner)
        {
            Division division = Instantiate(_division, transform);
            division.Construct(owner, target);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Division division))
            {
                OnDamageTaken?.Invoke(division);
            }
        }
    }
}
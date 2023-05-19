using System;
using DI;
using TMPro;
using UnityEngine;

namespace Level.Region.Views
{
    public class GarrisonView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _countText;

        [Header("Parameters")]
        [SerializeField] private float _garrisonIncreaseRate;
        [SerializeField] private int _garrisonInitialCount;

        public float GarrisonIncreaseRate => _garrisonIncreaseRate;
        public int GarrisonInitialCount => _garrisonInitialCount;

        public event Action OnDamageTaken;
        public event Action OnGarrisonRelease;

        public void SetCount(int count)
        {
            _countText.text = count.ToString();
        }

        public void Release()
        {
            OnGarrisonRelease?.Invoke();
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new(typeof(GarrisonView), () => this));
        }

        private void OnTriggerEnter(Collider other)
        {
            OnDamageTaken?.Invoke();
        }
    }
}
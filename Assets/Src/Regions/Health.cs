using Src.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Regions
{
    public class Health : MonoBehaviour, IDamageable
    {
        [Header("Parameters")]
        [SerializeField] private float _maxHealth = 50;
        [Header("Events")]
        [SerializeField] private UnityEvent OnOutOfHealth;

        private float _currentHealth;

        private Coroutine _regenerationCoroutine;

        public void Decrease(int amount = 1)
        {
            float decreasedHealth = _currentHealth - amount;

            if (decreasedHealth <= 0)
            {
                _currentHealth = 0;
                OnOutOfHealth.Invoke();
                return;
            }
            
            _currentHealth = decreasedHealth;
        }
        
        public void TakeDamage()
        {
            Decrease();
        }

        public bool IsDead()
        {
            return _currentHealth == 0;
        }
        
        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        private void Increase(float amount = 1)
        {
            float increasedHealth = _currentHealth + amount;

            if (increasedHealth >= _maxHealth)
            {
                _currentHealth = _maxHealth;
                StopRegenerating();
                return;
            }
            
            _currentHealth = increasedHealth;
        }

        private void StopRegenerating()
        {
            StopCoroutine(_regenerationCoroutine);
        }
    }
}
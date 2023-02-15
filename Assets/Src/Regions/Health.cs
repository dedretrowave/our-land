using System.Collections;
using Src.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Regions
{
    public class Health : MonoBehaviour, IDamageable
    {
        [Header("Parameters")]
        [SerializeField] private float _maxHealth = 50;
        [SerializeField] private float _regenerationPerSec = 1;
        [SerializeField] private float _regenerationTimeout = 5;
        [Header("Events")]
        [SerializeField] private UnityEvent OnOutOfHealth;
        [SerializeField] private UnityEvent OnHealthIncrease;
        [SerializeField] private UnityEvent OnHealthDecrease;

        private float _currentHealth;

        private Coroutine _regenerationCoroutine;

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void Decrease(int amount = 1)
        {
            float decreasedHealth = _currentHealth - amount;

            if (amount <= 0)
            {
                _currentHealth = 0;
                OnOutOfHealth.Invoke();
                return;
            }
            
            OnHealthDecrease.Invoke();
            _currentHealth = decreasedHealth;
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

            OnHealthIncrease.Invoke();
            _currentHealth = increasedHealth;
        }

        private IEnumerator StartRegeneratingAfterTimeout()
        {
            yield return new WaitForSeconds(_regenerationTimeout);
            
            StartRegenerating();
        }

        private void StartRegenerating()
        {
            _regenerationCoroutine = StartCoroutine(RegenerateAfterTimeout());
        }

        private void StopRegenerating()
        {
            StopCoroutine(_regenerationCoroutine);
        }

        private IEnumerator RegenerateAfterTimeout()
        {
            float frameRate = 60f;
            
            yield return new WaitForSeconds(1f / frameRate);

            Regenerate();

            yield return RegenerateAfterTimeout();
        }

        private void Regenerate()
        {
            float frameRate = 60f;
            
            Increase(_regenerationPerSec / frameRate);
        }

        public void TakeDamage()
        {
            Decrease();
        }
    }
}
using DI;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Helpers
{
    public class Pauser : MonoBehaviour
    {
        public UnityEvent OnGamePaused;
        public UnityEvent OnGameResumed;
        
        public void Pause()
        {
            Time.timeScale = 0;
            OnGamePaused.Invoke();
        }

        public void Resume()
        {
            Time.timeScale = 1;
            OnGameResumed.Invoke();
        }
        
        private void Start()
        {
            DontDestroyOnLoad(this);
            
            DependencyContext.Dependencies.Add(typeof(Pauser), () => this);
            
            // GameDistribution.OnResumeGame += Resume;
            // GameDistribution.OnPauseGame += Pause;
        }

        private void OnDisable()
        {
            // GameDistribution.OnResumeGame -= Resume;
            // GameDistribution.OnPauseGame -= Pause;
        }
    }
}
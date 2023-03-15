using Src.DI;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Levels.Level.Initialization
{
    public class LevelPlayStateWatcher : MonoBehaviour
    {
        public UnityEvent<Transform> OnLevelStarted;
        public UnityEvent OnLevelFinished;

        private void Start()
        {
            DependencyContext.Dependencies.Add(typeof(LevelPlayStateWatcher), () => this);
        }

        public void TriggerOnLevelStarted(Transform levelStarted)
        {
            OnLevelStarted.Invoke(levelStarted);
        }

        public void TriggerOnLevelFinished()
        {
            OnLevelFinished.Invoke();
        }
    }
}
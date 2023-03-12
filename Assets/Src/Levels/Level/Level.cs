using UnityEngine;
using UnityEngine.Events;

namespace Src.Levels.Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform _levelPrefab;

        [SerializeField] private UnityEvent<LevelStatus> _onStatusChange;

        private LevelStatus _status = LevelStatus.Uncompleted;

        public LevelStatus Status => _status;
        public Transform Prefab => _levelPrefab;

        public void SetStatus(LevelStatus newStatus)
        {
            _status = newStatus;
            _onStatusChange.Invoke(_status);
        }
    }
}
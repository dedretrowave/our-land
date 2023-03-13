using System;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Levels.Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform _levelPrefab;

        [SerializeField] private UnityEvent<LevelStatus> _onStatusChange = new();
        [SerializeField] private UnityEvent _onWon = new();
        [SerializeField] private UnityEvent _onFail = new();

        private LevelStatus _status = LevelStatus.Uncompleted;

        public LevelStatus Status => _status;
        public Transform Prefab => _levelPrefab;

        private void SetStatus(LevelStatus newStatus)
        {
            _status = newStatus;

            switch (_status)
            {
                case LevelStatus.Completed:
                    _onWon.Invoke();
                    break;
                case LevelStatus.Uncompleted:
                    _onFail.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            _onStatusChange.Invoke(_status);
        }

        private void Start()
        {
            SetStatus(_status);
        }
    }
}
using UnityEngine;

namespace Src.Levels.Level.Initialization
{
    public class LevelSpawner : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private Transform _levelPrefab;

        private Transform _level;

        public void Spawn()
        {
            if (_level != null) return;
            
            _level = Instantiate(_levelPrefab, transform);
        }

        public void Remove()
        {
            if (_level == null) return;
            
            Destroy(_level.gameObject);
        }
    }
}
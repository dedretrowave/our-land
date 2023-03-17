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
            _level = Instantiate(_levelPrefab, transform);
        }

        public void Remove()
        {
            Destroy(_level.gameObject);
        }
    }
}
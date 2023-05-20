using UnityEngine;

namespace Src.Levels.Level.Initialization
{
    public class LevelSpawner : MonoBehaviour
    {
        [Header("Level")]
        [SerializeField] private Level _level;
        
        [Header("Prefab")]
        [SerializeField] private RegionOwnerInitializer _levelPrefab;

        private RegionOwnerInitializer _regionInitializer;

        public void Spawn()
        {
            if (_regionInitializer != null) return;
            
            _regionInitializer = Instantiate(_levelPrefab, transform);
            // _regionInitializer.Init(_level.Owner.Id);
        }

        public void Remove()
        {
            if (_regionInitializer == null) return;
            
            Destroy(_regionInitializer.gameObject);
        }
    }
}
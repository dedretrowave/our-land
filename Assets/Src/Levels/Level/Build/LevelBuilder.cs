using Src.Models.Level;
using Src.Regions;
using Src.Regions.Containers;
using UnityEngine;

namespace Src.Levels.Level.Build
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private LevelData _level;
        [SerializeField] private RegionContainer _playerContainer;
        [SerializeField] private RegionContainer _enemyContainer;

        private void Awake()
        {
            _level.Regions.ForEach(region =>
            {
                Region newRegion = Instantiate(region.RegionPrefab);

                newRegion.SetData(region);
            });
        }
    }
}
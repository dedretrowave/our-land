using System.Collections.Generic;
using Src.Models.Region;
using UnityEngine;

namespace Src.Models.Level
{
    [CreateAssetMenu(fileName = "Level", menuName = "Level/level", order = 0)]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private List<RegionData> _regions;

        public List<RegionData> Regions => _regions;
    }
}
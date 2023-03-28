using System.Collections.Generic;
using UnityEngine;

namespace Src.Levels
{
    public class LevelContainer : MonoBehaviour
    {
        [SerializeField] private List<Level.Level> _levels;

        public List<Level.Level> Levels => _levels;
    }
}
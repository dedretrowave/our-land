using System;
using System.Collections.Generic;
using Src.Global;
using Src.Levels.Level;
using UnityEngine;

namespace Src.Settings
{
    public class ColorsSettings : MonoBehaviour
    {
        [SerializeField] private List<ColorToLevelStatus> _colorToLevelStatus;

        private static ColorsSettings _instance;

        public Color GetColorByLevelStatus(LevelCompletionState status)
        {
            return _colorToLevelStatus.Find(color => color.Status == status).Color;
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(typeof(ColorsSettings), () => this);
        }
    }

    [Serializable]
    internal class ColorToLevelStatus
    {
        public Color Color;
        public LevelCompletionState Status;
    }
}
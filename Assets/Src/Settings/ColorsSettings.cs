using System;
using System.Collections.Generic;
using Src.Global;
using Src.Levels.Level;
using UnityEngine;

namespace Src.Settings
{
    public class ColorsSettings : MonoBehaviour
    {
        [SerializeField] private List<ColorToFraction> _colorsToFractions;
        [SerializeField] private List<ColorToLevelStatus> _colorToLevelStatus;

        private static ColorsSettings _instance;

        public Color GetColorByFraction(Fraction.Fraction fraction)
        {
            return _colorsToFractions.Find(entity => entity.Fraction == fraction).Color;
        }

        public Color GetColorByLevelStatus(LevelStatus status)
        {
            return _colorToLevelStatus.Find(color => color.Status == status).Color;
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(typeof(ColorsSettings), () => this);
        }
    }

    [Serializable]
    internal class ColorToFraction
    { 
        public Color Color; 
        public Fraction.Fraction Fraction;
    }

    [Serializable]
    internal class ColorToLevelStatus
    {
        public Color Color;
        public LevelStatus Status;
    }
}
using System;
using System.Collections.Generic;
using Src.Regions.Fraction;
using UnityEngine;

namespace Src.Global
{
    public class ColorsToFractions : MonoBehaviour
    {
        [SerializeField] private List<Entity> _entities;

        private static ColorsToFractions _instance;

        public Color GetColorByFraction(Fraction fraction)
        {
            return _entities.Find(entity => entity.Fraction == fraction).Color;
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(typeof(ColorsToFractions), () => this);
        }
    }

    [Serializable]
    internal class Entity
    {
        public Color Color;
        public Fraction Fraction;
    }
}
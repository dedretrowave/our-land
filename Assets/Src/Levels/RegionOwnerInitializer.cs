using System;
using System.Collections.Generic;
using System.Reflection;
using DI;
using Src.Map.Fraction;
using Src.Map.Regions;
using Src.SkinShop.Skin;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Src.Levels
{
    public class RegionOwnerInitializer : MonoBehaviour
    {
        [SerializeField] private List<Region> _enemyRegions;

        public void Init(int enemyId)
        {
            Character enemy = DependencyContext.Dependencies.Get<FractionContainer>().GetFractionById(enemyId);

            _enemyRegions.ForEach(region =>
            {
                region.Init(enemy);
            });
        }
    }
}
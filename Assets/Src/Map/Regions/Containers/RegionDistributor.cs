using System;
using System.Collections.Generic;
using Src.DI;
using UnityEngine;

namespace Src.Map.Regions.Containers
{
    public class RegionDistributor : MonoBehaviour
    {
        [SerializeField] private List<ContainersByFractions> _containers = new();

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new Dependency(typeof(RegionDistributor), () => this));
        }

        public void DistributeRegion(Region region, Fraction.Fraction newOwner)
        {
            RegionContainer container = _containers.Find(container => container.Fraction == newOwner).Container;

            region.SetContainer(container);
        }
    }

    [Serializable]
    internal class ContainersByFractions
    {
        public Fraction.Fraction Fraction;
        public RegionContainer Container;
    }
}
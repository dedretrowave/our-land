using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Regions.Containers
{
    public class RegionContainer : MonoBehaviour
    {
        [SerializeField] private List<Region> _regions;

        [SerializeField] private UnityEvent _onEmpty;

        public void RemoveRegion(Region region)
        {
            _regions.Remove(region);

            if (_regions.Count == 0)
            {
                _onEmpty.Invoke();
            }
        }

        public void AddRegion(Region region)
        {
            _regions.Add(region);
        }
    }
}
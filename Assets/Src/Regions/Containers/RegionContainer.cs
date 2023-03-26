using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Regions.Containers
{
    public class RegionContainer : MonoBehaviour
    {
        [SerializeField] private List<Region> _regions;

        public UnityEvent OnEmpty = new();

        public Region GetRandomRegion()
        {
            return _regions.Count == 0 ? null : _regions[Random.Range(0, _regions.Count)];
        }

        public void RemoveRegion(Region region)
        {
            _regions.Remove(region);

            if (_regions.Count == 0)
            {
                OnEmpty.Invoke();
            }
        }

        public void Clear()
        {
            _regions.Clear();
        }

        public void AddRegion(Region region)
        {
            _regions.Add(region);
        }
    }
}
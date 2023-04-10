using System.Collections.Generic;
using Src.Map.Fraction;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Map.Regions.Containers
{
    public class RegionContainer : MonoBehaviour
    {
        [SerializeField] private Character _owner;
        [SerializeField] private List<Region> _regions;

        public Character Owner
        {
            get => _owner;
            set => _owner = value;
        }
        public UnityEvent OnEmpty = new();

        public bool HasRegion(Region region)
        {
            return _regions.Contains(region);
        }

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

            _regions.RemoveAll(item => item == null);
        }
    }
}
using DI;
using Entries;
using Level;
using Map;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Components
{
    public class LevelSelector : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private LevelConfig _prefab;

        private MapRegionInstaller _mapRegion;
        
        public void Construct(MapRegionInstaller mapRegion)
        {
            _mapRegion = mapRegion;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            LevelEntryPoint entryPoint = DependencyContext.Dependencies.Get<LevelEntryPoint>();
            
            entryPoint.Init(_prefab, _mapRegion);
        }
        
        private void Start() {}
    }
}
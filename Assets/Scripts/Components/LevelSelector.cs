using DI;
using Entries;
using Level;
using Region.Models;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Components
{
    public class LevelSelector : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private LevelConfig _prefab;
        
        private RegionModel _model;
        
        public void Construct(RegionModel model)
        {
            _model = model;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            LevelEntryPoint entryPoint = DependencyContext.Dependencies.Get<LevelEntryPoint>();
            
            entryPoint.Init(_prefab, _model);
        }
        
        private void Start() {}
    }
}
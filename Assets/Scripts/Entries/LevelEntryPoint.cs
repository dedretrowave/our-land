using DI;
using Level;
using Region.Models;
using UnityEngine;

namespace Entries
{
    public class LevelEntryPoint : MonoBehaviour
    {
        public void Init(LevelInstaller levelPrefab, RegionModel model)
        {
            LevelInstaller level = Instantiate(levelPrefab, transform);
            
            level.Construct(model.CurrentOwner);
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new(typeof(LevelEntryPoint), () => this));
        }
    }
}
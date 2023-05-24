using DI;
using Level;
using UnityEngine;

namespace Entries
{
    public class LevelEntryPoint : MonoBehaviour
    {
        private LevelInstaller _levelInstaller;

        private void Start()
        {
            _levelInstaller = DependencyContext.Dependencies.Get<LevelInstaller>();
            
            // _levelInstaller.Construct();
        }
    }
}
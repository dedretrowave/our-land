using DI;
using Level;
using UnityEngine;

namespace Test
{
    public class LevelEntryPoint : MonoBehaviour
    {
        private LevelInstaller _levelInstaller;

        private void Start()
        {
            _levelInstaller = DependencyContext.Dependencies.Get<LevelInstaller>();
            
            _levelInstaller.Construct();
        }
    }
}
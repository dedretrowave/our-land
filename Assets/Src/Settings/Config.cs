using DI;
using UnityEngine;

namespace Src.Settings
{
    public class Config : MonoBehaviour
    {
        public float DivisionChangeRateInSeconds = 0.02f;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            DependencyContext.Dependencies.Add(typeof(Config), () => this);
        }
    }
}
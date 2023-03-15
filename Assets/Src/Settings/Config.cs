using Src.DI;
using UnityEngine;

namespace Src.Settings
{
    public class Config : MonoBehaviour
    {
        public float DivisionChangeRateInSeconds = 0.02f;

        private void Awake()
        {
            DependencyContext.Dependencies.Add(typeof(Config), () => this);
        }
    }
}
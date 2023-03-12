using Src.Global;
using UnityEngine;

namespace Src.Settings
{
    public class Config : MonoBehaviour
    {
        public float DivisionChangeRateInSeconds = 0.02f;
        public float TapDurationInSeconds = 0.1f;

        private void Awake()
        {
            DependencyContext.Dependencies.Add(typeof(Config), () => this);
        }
    }
}
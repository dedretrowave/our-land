using System;
using UnityEngine;

namespace Src.Global
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
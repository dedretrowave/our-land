using UnityEngine;

namespace Src.Map.Fraction
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Fraction _fraction;

        public Fraction Fraction
        {
            get => _fraction;
            set => _fraction = value;
        }
    }
}
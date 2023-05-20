using UnityEngine;

namespace Src.Map.Fraction
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Character _fraction;

        public Character Fraction
        {
            get => _fraction;
            set => _fraction = value;
        }
    }
}
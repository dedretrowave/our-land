using UnityEngine;

namespace Src.Fraction
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Fraction _fraction;

        public Fraction Fraction => _fraction;
    }
}
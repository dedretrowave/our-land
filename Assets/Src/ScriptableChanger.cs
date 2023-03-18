using Src.Fraction;
using UnityEngine;

namespace Src
{
    public class ScriptableChanger : MonoBehaviour
    {
        [SerializeField] private Fraction.Fraction _character;
        [SerializeField] private Sprite _sprite;

        private void Start()
        {
            _character.Flag = _sprite;
        }
    }
}
using Characters.Base;
using UnityEngine;

namespace Components
{
    public class Division : MonoBehaviour
    {
        private Character _character;

        public Character Owner => _character;

        public void Construct(Character owner)
        {
            _character = owner;
        }
    }
}
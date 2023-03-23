using UnityEngine;

namespace Src.SkinShop
{
    public class PlayerSkinChanger : MonoBehaviour
    {
        [SerializeField] private Fraction.Fraction _player;

        public void SetFlag(Sprite flag)
        {
            _player.Flag = flag;
        }

        public void SetEyes(Sprite eyes)
        {
            _player.Eyes = eyes;
        }
    }
}
using System;
using Animations;
using Characters.Base;
using Characters.Skins;
using UnityEngine;
using UnityEngine.UI;

namespace Region.Views
{
    public class RegionView : MonoBehaviour
    {
        private CharacterAnimations _animations;
        
        [Header("Components")]
        [Header("UI")]
        [SerializeField] private Image _region;
        [SerializeField] private Image _flag;
        [SerializeField] private Image _eyes;
        
        public event Action<RegionView, Character, Character> OnOwnerChange;

        public void NotifyOwnerChange(Character oldOwner, Character newOwner)
        {
            OnOwnerChange?.Invoke(this, oldOwner, newOwner);
        }

        public void SetVFXByCharacter(Character owner)
        {
            SetColor(owner.Color);
            SetSkin(owner.Skin);
        }

        public void SetColor(Color color)
        {
            _region.color = color;
        }

        public void SetSkin(Skin skin)
        {
            _flag.sprite = skin.GetItemByType(SkinItemType.Flag).Sprite;
            _eyes.sprite = skin.GetItemByType(SkinItemType.Eyes).Sprite;
        }
        
        public void PlayHurt()
        {
            _animations.PlayHurt();
        }
        
        private void Awake()
        {
            _animations = GetComponentInChildren<CharacterAnimations>();
        }
    }
}
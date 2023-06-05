using System;
using Animations;
using Characters.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Region.Views
{
    public class RegionView : MonoBehaviour
    {
        [Header("Components")]
        [Header("UI")]
        [SerializeField] private Image _region;

        public event Action<RegionView, CharacterModel, CharacterModel> OnOwnerChange;

        public void NotifyOwnerChange(CharacterModel oldOwner, CharacterModel newOwner)
        {
            OnOwnerChange?.Invoke(this, oldOwner, newOwner);
        }

        public void SetVFXByCharacter(CharacterModel owner)
        {
            SetColor(owner.Color);
        }

        public void SetColor(Color color)
        {
            _region.color = color;
        }

        private void Awake()
        {
            GetComponentInChildren<CharacterAnimations>();
        }
    }
}
using Animations;
using Characters.Skins;
using Characters.Skins.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Characters.View
{
    public class CharacterView : MonoBehaviour
    {
        private CharacterAnimations _animations;
        
        [SerializeField] private Image _flag;
        [SerializeField] private Image _eyes;

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
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
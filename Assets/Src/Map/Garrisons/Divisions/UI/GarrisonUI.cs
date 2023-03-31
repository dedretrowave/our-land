using Src.DI;
using Src.SkinShop;
using Src.SkinShop.Items.Base;
using Src.SkinShop.Skin;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Map.Garrisons.Divisions.UI
{
    public class GarrisonUI : MonoBehaviour
    {
        [SerializeField] private Image _flag;
        [SerializeField] private Image _eyes;

        private FractionSkinContainer _fractionSkinContainer;

        public void UpdateByFraction(Fraction.Fraction fraction)
        {
            FractionSkin skin = _fractionSkinContainer.GetSkinByFraction(fraction);
            UpdateByFraction(skin);
        }

        public void UpdateByFraction(FractionSkin skin)
        {
            _flag.sprite = skin.Skin.GetItemByType(SkinItemType.Flag).Sprite;
            _eyes.sprite = skin.Skin.GetItemByType(SkinItemType.Eyes).Sprite;
        }

        private void Start()
        {
            _fractionSkinContainer = DependencyContext.Dependencies.Get<FractionSkinContainer>();
        }
    }
}
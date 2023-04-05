using Src.DI;
using Src.Levels.Level;
using Src.SkinShop.Items;
using Src.SkinShop.Items.Base;
using Src.SkinShop.Skin;
using Src.SkinShop.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Map.Garrisons.Divisions.UI
{
    public class GarrisonUI : MonoBehaviour
    {
        [SerializeField] private SkinUI _skinUI;

        private FractionContainer _fractionSkinContainer;

        public void UpdateByLevel(Level level)
        {
            FractionSkinHolder skinHolder = _fractionSkinContainer.GetSkinByFraction(level.Owner);
            UpdateByFraction(skinHolder);
        }

        public void UpdateByFraction(Fraction.Fraction fraction)
        {
            FractionSkinHolder skinHolder = _fractionSkinContainer.GetSkinByFraction(fraction);
            UpdateByFraction(skinHolder);
        }

        public void UpdateByFraction(FractionSkinHolder skinHolder)
        {
            skinHolder.OnSkinChanged.AddListener(UpdateSkin);
            UpdateSkin(skinHolder.Skin);
        }

        private void UpdateSkin(Skin skin)
        {
            _skinUI.SetSkin(skin);
        }

        private void Start()
        {
            _fractionSkinContainer = DependencyContext.Dependencies.Get<FractionContainer>();
        }
    }
}
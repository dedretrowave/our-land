using Characters.Skins;
using DI;
using Src.Levels.Level;
using Src.Map.Fraction;
using Src.SkinShop.Skin;
using Src.SkinShop.UI;
using UnityEngine;

namespace Src.Map.Garrisons.Divisions.UI
{
    public class GarrisonUI : MonoBehaviour
    {
        [SerializeField] private SkinUI _skinUI;

        private FractionContainer _fractionSkinContainer;
        private FractionSkinHolder _skinHolder;

        public void UpdateByLevel(Levels.Level.Level level)
        {
            if (_fractionSkinContainer == null) _fractionSkinContainer = DependencyContext.Dependencies.Get<FractionContainer>();

            FractionSkinHolder skinHolder = _fractionSkinContainer.GetSkinByFraction(level.Owner);
            UpdateByFraction(skinHolder);
        }

        public void UpdateByFraction(Character fraction)
        {
            if (_fractionSkinContainer == null) _fractionSkinContainer = DependencyContext.Dependencies.Get<FractionContainer>(); 
            
            FractionSkinHolder skinHolder = _fractionSkinContainer.GetSkinByFraction(fraction);
            UpdateByFraction(skinHolder);
        }

        public void UpdateByFraction(FractionSkinHolder skinHolder)
        {
            if (_skinHolder != null) _skinHolder.OnSkinChanged.RemoveListener(UpdateSkin);
                
            _skinHolder = skinHolder;
            _skinHolder.OnSkinChanged.AddListener(UpdateSkin);
            // UpdateSkin(skinHolder.Skin);
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
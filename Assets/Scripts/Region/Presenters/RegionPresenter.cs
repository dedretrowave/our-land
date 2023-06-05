using System;
using Characters.Model;
using Characters.View;
using Region.Models;
using Region.Views;

namespace Region.Presenters
{
    public class RegionPresenter
    {
        private RegionView _regionView;
        private CharacterView _characterView;
        
        private RegionModel _regionModel;

        public event Action<CharacterModel, CharacterModel> OnOwnerChange;

        public RegionPresenter(
            CharacterModel defaultOwner,
            RegionView regionView,
            RegionModel regionModel,
            CharacterView characterView)
        {
            _regionView = regionView;
            _regionModel = regionModel;
            _characterView = characterView;
            ChangeOwner(defaultOwner);
            _characterView.SetSkin(_regionModel.CurrentOwner.Skin);
        }

        public void TakeDamage()
        {
            _characterView.PlayHurt();
        }

        public void ChangeOwner(CharacterModel newOwner)
        {
            CharacterModel oldOwner = _regionModel.CurrentOwner;
            _regionModel.SetOwner(newOwner);
            _characterView.SetSkin(_regionModel.CurrentOwner.Skin);
            _regionView.SetColor(_regionModel.CurrentOwner.Color);
            OnOwnerChange?.Invoke(oldOwner, _regionModel.CurrentOwner);
        }
    }

    internal class GarrisonIsZeroException : Exception { }
}
using System;
using Characters.Model;
using Region.Models;
using Region.Views;

namespace Map.Presenters
{
    public class MapRegionPresenter
    {
        private RegionView _regionView;

        private RegionModel _regionModel;

        public event Action<CharacterModel> OnOwnerChange; 

        public MapRegionPresenter(
            RegionView view,
            RegionModel model
        )
        {
            _regionModel = model;
            _regionView = view;
            
            SetOwner(_regionModel.CurrentOwner);
        }

        public void SetOwner(CharacterModel newOwner)
        {
            _regionModel.SetOwner(newOwner);
            _regionView.SetVFXByCharacter(_regionModel.CurrentOwner);
            OnOwnerChange?.Invoke(_regionModel.CurrentOwner);
        }
    }
}
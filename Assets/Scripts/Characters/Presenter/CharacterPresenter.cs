using Characters.Model;
using Characters.Skins;
using Characters.View;
using UnityEngine;

namespace Characters.Presenter
{
    public class CharacterPresenter
    {
        private CharacterView _view;

        private CharacterModel _model;

        public bool IsPlayer => _model.Fraction == Fraction.Fraction.Player;

        public CharacterPresenter(
            CharacterView view,
            CharacterModel model)
        {
            _view = view;
            _model = model;

            _view.SetSkin(_model.Skin);
        }

        public void SetSkinByCharacter(CharacterModel character)
        {
            SetSkin(character.Skin);
        }
        
        public void SetSkin(Skin skin)
        {
            _model.SetSkin(skin);
            _view.SetSkin(_model.Skin);
        }
    }
}
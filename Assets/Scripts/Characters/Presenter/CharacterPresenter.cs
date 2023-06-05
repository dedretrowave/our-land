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
            _view.SetSkin(character.Skin);
        }
    }
}
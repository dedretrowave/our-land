using Characters;
using Characters.Skins;
using DI;
using Level.Models;
using Map.UI.Views;

namespace Map.UI.Presenters
{
    public class LevelFinishedPresenter
    {
        private CharacterContainer _characterContainer;
        
        private LevelModel _levelModel;

        private LevelFinishedView _view;

        private Skin _playerSkin;

        public LevelFinishedPresenter(LevelFinishedView view)
        {
            _characterContainer = DependencyContext.Dependencies.Get<CharacterContainer>();
            _playerSkin = _characterContainer.GetByFraction(Fraction.Fraction.Player).Skin;

            _view = view;
            _view.SetSkin(_playerSkin);
        }

        public void DisplayReward(int reward)
        {
            _view.ShowWin();
            _view.DisplayReward(reward);
        }
        
        public void TriggerLose()
        {
            _view.ShowLose();
        }
    }
}
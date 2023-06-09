using Level.Models;
using Map.UI.Views;

namespace Map.UI.Presenters
{
    public class LevelFinishedPresenter
    {
        private LevelModel _levelModel;
        
        private LevelFinishedView _view;

        public LevelFinishedPresenter(LevelFinishedView view)
        {
            _view = view;
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
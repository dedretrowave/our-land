using System;
using Level.Models;
using Level.Presenters;
using Map.UI.Views;

namespace Map.UI.Presenters
{
    public class LevelFinishedPresenter
    {
        private LevelModel _levelModel;
        
        private LevelFinishedView _view;

        public LevelFinishedPresenter(LevelFinishedView view, LevelModel levelModel)
        {
            _levelModel = levelModel;
            _view = view;
        }

        public void TriggerByLevelEnd(LevelStatus status)
        {
            switch (status)
            {
                case LevelStatus.Win:
                    TriggerWin();
                    break;
                case LevelStatus.Lose:
                    TriggerLose();
                    break;
                case LevelStatus.InProgress:
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        private void TriggerWin()
        {
            _view.ShowWin();
            _view.DisplayReward(_levelModel.Reward);
        }

        private void TriggerLose()
        {
            _view.ShowLose();
        }
    }
}
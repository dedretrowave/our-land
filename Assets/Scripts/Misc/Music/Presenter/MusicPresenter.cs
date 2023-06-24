using Misc.Music.Data;
using Misc.Music.Model;
using Misc.Music.View;
using Save;

namespace Misc.Music.Presenter
{
    public class MusicPresenter
    {
        private const string LoadPath = "music";
        private SaveFileHandler _saveFileHandler;
        
        private MusicModel _model;
        private MusicSettings _settings;

        private MusicView _view;

        public bool IsEnabled => _model.IsEnabled;

        public MusicPresenter(MusicView view, MusicSettings settings)
        {
            _saveFileHandler = new();

            MusicModel model = _saveFileHandler.Load<MusicModel>(LoadPath) 
                               ?? new(true);

            _settings = settings;
            _model = model;
            
            _view = view;
            
            _view.SetEnabled(_model.IsEnabled);
            _view.SetTrack(_settings.GetDefault());
        }

        public void SetEnabled(bool value)
        {
            _model.SetEnabled(value);
            _saveFileHandler.Save(LoadPath, _model);
            _view.SetEnabled(_model.IsEnabled);
        }

        public void SetMapTrack()
        {
            _view.SetTrack(_settings.GetByType(TrackType.Map));
        }

        public void SetLevelTrack()
        {
            _view.SetTrack(_settings.GetByType(TrackType.Level));
        }

        public void SetWinTrack()
        {
            _view.SetTrack(_settings.GetByType(TrackType.Success));
        }

        public void SetLoseTrack()
        {
            _view.SetTrack(_settings.GetByType(TrackType.Failure));
        }
    }
}
using Components.Music.Data;
using Components.Music.Presenter;
using Components.Music.View;
using EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Music
{
    public class MusicInstaller : MonoBehaviour
    {
        [SerializeField] private MusicView _view;
        [SerializeField] private MusicSettings _settings;
        [SerializeField] private Toggle _toggle;

        private EventBus.EventBus _eventBus;

        private MusicPresenter _presenter;

        public void Construct()
        {
            _eventBus = EventBus.EventBus.Instance;

            _presenter = new(_view, _settings);

            if (_toggle != null)
            {
                _toggle.isOn = _presenter.IsEnabled;

                _toggle.onValueChanged.AddListener(_presenter.SetEnabled);
            }

            _eventBus.AddListener(EventName.ON_LEVEL_STARTED, _presenter.SetLevelTrack);
            _eventBus.AddListener(EventName.ON_LEVEL_WON, _presenter.SetWinTrack);
            _eventBus.AddListener(EventName.ON_LEVEL_LOST, _presenter.SetLoseTrack);
            _eventBus.AddListener(EventName.ON_MAP_OPENED, _presenter.SetMapTrack);
            _eventBus.AddListener(EventName.ON_REWARDED_OPENED, _presenter.Mute);
            _eventBus.AddListener(EventName.ON_REWARDED_WATCHED, _presenter.Unmute);
            _eventBus.AddListener(EventName.ON_REWARDED_SKIPPED, _presenter.Unmute);
        }

        public void Disable()
        {
            if (_toggle != null)
            {
                _toggle.onValueChanged.RemoveListener(_presenter.SetEnabled);
            }

            _eventBus.RemoveListener(EventName.ON_LEVEL_STARTED, _presenter.SetLevelTrack);
            _eventBus.RemoveListener(EventName.ON_LEVEL_WON, _presenter.SetWinTrack);
            _eventBus.RemoveListener(EventName.ON_LEVEL_LOST, _presenter.SetLoseTrack);
            _eventBus.RemoveListener(EventName.ON_MAP_OPENED, _presenter.SetMapTrack);
            _eventBus.RemoveListener(EventName.ON_REWARDED_OPENED, _presenter.Mute);
            _eventBus.RemoveListener(EventName.ON_REWARDED_WATCHED, _presenter.Unmute);
            _eventBus.RemoveListener(EventName.ON_REWARDED_SKIPPED, _presenter.Unmute);
        }
    }
}
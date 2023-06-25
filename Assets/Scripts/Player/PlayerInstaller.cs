using Characters.Model;
using Characters.Skins;
using Characters.View;
using EventBus;
using Player.Presenter;
using UnityEngine;

namespace Player
{
    public class PlayerInstaller : MonoBehaviour
    {
        [SerializeField] private CharacterView _playerView;

        private EventBus.EventBus _eventBus;

        private PlayerPresenter _presenter;
        
        public void Construct()
        {
            _eventBus = EventBus.EventBus.Instance;

            _presenter = new PlayerPresenter(_playerView);

            _presenter.OnSkinChange += TriggerSkinChange;
            _presenter.OnModelCreated += TriggerModelCreated;
            
            _presenter.Init();
            
            _eventBus
                .AddListener<Skin>(
                    EventName.ON_SKIN_IN_SHOP_SELECTED,
                    _presenter.SetSkin);
            _eventBus
                .AddListener(
                    EventName.ON_LEVEL_STARTED,
                    _presenter.Hide);
            _eventBus
                .AddListener(
                    EventName.ON_LEVEL_ENDED,
                    _presenter.Show);
        }

        public void Disable()
        {
            _presenter.OnSkinChange -= TriggerSkinChange;
            _presenter.OnModelCreated -= TriggerModelCreated;
            
            _eventBus
                .RemoveListener<Skin>(
                    EventName.ON_SKIN_IN_SHOP_SELECTED,
                    _presenter.SetSkin);
            _eventBus
                .RemoveListener(
                    EventName.ON_LEVEL_STARTED,
                    _presenter.Hide);
            _eventBus
                .RemoveListener(
                    EventName.ON_LEVEL_ENDED,
                    _presenter.Show);
        }

        public Skin GetSkin()
        {
            return _presenter.GetCurrentSkin();
        }

        private void TriggerSkinChange(Skin skin)
        {
            _eventBus.TriggerEvent(EventName.ON_CHARACTER_SKIN_CHANGE, skin);
        }

        private void TriggerModelCreated(CharacterModel model)
        {
            _eventBus.TriggerEvent(EventName.ON_PLAYER_MODEL_CREATED, model);
        }
    }
}
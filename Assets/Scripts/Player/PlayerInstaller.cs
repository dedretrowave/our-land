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
            
            _eventBus
                .AddListener<Skin>(
                    EventName.ON_SKIN_IN_SHOP_SELECTED,
                    _presenter.SetSkin);
        }

        public void Disable()
        {
            _presenter.OnSkinChange -= TriggerSkinChange;
            _eventBus
                .RemoveListener<Skin>(
                    EventName.ON_SKIN_IN_SHOP_SELECTED,
                    _presenter.SetSkin);
        }

        public Skin GetSkin()
        {
            return _presenter.GetCurrentSkin();
        }

        private void TriggerSkinChange(Skin skin)
        {
            _eventBus.TriggerEvent(EventName.ON_CHARACTER_SKIN_CHANGE, skin);
        }
    }
}
using EventBus;
using UnityEngine;

namespace Misc
{
    public class EventBusTrigger : MonoBehaviour
    {
        private EventBus.EventBus _eventBus;

        private void Start()
        {
            _eventBus = EventBus.EventBus.Instance;
        }

        public void TriggerOpenMap()
        {
            _eventBus.TriggerEvent(EventName.ON_MAP_OPENED);
        }
    }
}
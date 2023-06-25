using EventBus;
using UnityEngine;

namespace Components.Ads
{
    public class Ads : MonoBehaviour
    {
        [SerializeField] private float _randomChanceOfAd;
        
        private EventBus.EventBus _eventBus;

        private void Start()
        {
            _eventBus = EventBus.EventBus.Instance;
            
            _eventBus.AddListener(EventName.ON_LEVEL_ENDED, ShowAdWithChance);
            _eventBus.AddListener(EventName.ON_REWARDED_OPENED, ShowRewarded);
            // GameDistribution.OnRewardedVideoSuccess += InvokeRewardedWatched;
            // GameDistribution.OnRewardedVideoFailure += InvokeRewardedSkipped;
        }

        public void ShowRewarded()
        {
            // _gameDistribution.ShowRewardedAd();
        }

        private void ShowAdWithChance()
        {
            if (Random.Range(0f, 100f) < _randomChanceOfAd)
            {
                ShowAd();
            }
        }

        private void OnDestroy()
        {
            // GameDistribution.OnRewardedVideoSuccess -= InvokeRewardedWatched;
            // GameDistribution.OnRewardedVideoFailure -= InvokeRewardedSkipped;
        }

        private void ShowAd()
        {
            // _gameDistribution.ShowAd();
        }

        private void InvokeRewardedWatched()
        {
            _eventBus.TriggerEvent(EventName.ON_REWARDED_WATCHED);
        }

        private void InvokeRewardedSkipped()
        {
            _eventBus.TriggerEvent(EventName.ON_REWARDED_SKIPPED);
        }
    }
}
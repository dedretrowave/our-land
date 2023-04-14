using Src.DI;
using Src.Helpers;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Ads
{
    public class Ads : MonoBehaviour
    {
        public UnityEvent OnRewardedAdWatched;
        public UnityEvent OnRewardedAdSkipped;

        public void ShowRewarded()
        {
            // _gameDistribution.ShowRewardedAd();
        }

        public void ShowAdWithChance(float chance = 50f)
        {
            if (Random.Range(0f, 100f) < chance)
            {
                ShowAd();
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(this);

            DependencyContext.Dependencies.Add(typeof(Ads), () => this);

            // GameDistribution.OnRewardedVideoSuccess += InvokeRewardedWatched;
            // GameDistribution.OnRewardedVideoFailure += InvokeRewardedSkipped;
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
            OnRewardedAdWatched.Invoke();
        }

        private void InvokeRewardedSkipped()
        {
            OnRewardedAdSkipped.Invoke();
        }
    }
}
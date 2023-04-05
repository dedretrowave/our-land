using Src.DI;
using UnityEngine;
using UnityEngine.Events;
using CrazyGames;

namespace Src.Ads
{
    public class Ads : MonoBehaviour
    {
        public UnityEvent OnRewardedAdWatched;
        public UnityEvent OnRewardedAdSkipped;

        public void ShowRewarded()
        {
            CrazyAds.Instance.beginAdBreakRewarded(InvokeRewardedWatched, InvokeRewardedSkipped);
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
        }

        private void ShowAd()
        {
            CrazyAds.Instance.beginAdBreak();
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
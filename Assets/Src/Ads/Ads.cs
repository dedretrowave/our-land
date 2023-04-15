using Src.DI;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Ads
{
    public class Ads : MonoBehaviour
    {
        [SerializeField] private GameMonetize _gameMonetize;
        
        public UnityEvent OnRewardedAdWatched;
        public UnityEvent OnRewardedAdSkipped;

        public void ShowRewarded()
        {
            _gameMonetize.ShowAd();
            InvokeRewardedWatched();
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
            _gameMonetize.ShowAd();
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
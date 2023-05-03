using System.Runtime.InteropServices;
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

        [DllImport("__Internal")]
        private static extern void ShowAdExternal();

        [DllImport("__Internal")]
        private static extern void ShowRewardedExternal();

        public void ShowRewarded()
        {
            ShowRewardedExternal();
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
            ShowAdExternal();
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
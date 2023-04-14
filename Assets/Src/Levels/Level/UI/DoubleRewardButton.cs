using UnityEngine;
using UnityEngine.UI;

namespace Src.Levels.Level.UI
{
    public class DoubleRewardButton : MonoBehaviour
    {
        [SerializeField] private Ads.Ads _ads;
        [SerializeField] private Button _button;

        private Level _completedLevel;

        public void BindLevelToRewardApplier(Level level)
        {
            _completedLevel = level;
            _button.onClick.AddListener(ShowAd);
            _button.onClick.AddListener(WrapUp);
        }

        private void Start()
        {
            _ads.OnRewardedAdWatched.AddListener(Apply);
        }

        private void WrapUp()
        {
            Unsubscribe();
            gameObject.SetActive(false);
        }

        private void Unsubscribe()
        {
            _button.onClick.RemoveListener(Apply);
        }

        private void ShowAd()
        {
            _ads.ShowRewarded();
        }

        private void Apply()
        {
            _completedLevel.Reward.Apply(_completedLevel);
        }
    }
}
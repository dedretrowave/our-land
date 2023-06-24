using Misc.Ads;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Levels.Level.UI
{
    public class DoubleRewardButton : MonoBehaviour
    {
        [SerializeField] private Ads _ads;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _rewardText;

        private Level _completedLevel;

        public void BindLevelToRewardApplier(Level level)
        {
            _completedLevel = level;
            _button.onClick.AddListener(ShowAd);
            _button.onClick.AddListener(WrapUp);
        }

        private void Start()
        {
            // _ads.OnRewardedAdWatched.AddListener(Apply);
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
            _rewardText.text = $"+{(_completedLevel.Reward.Amount * 2).ToString()}";
        }
    }
}
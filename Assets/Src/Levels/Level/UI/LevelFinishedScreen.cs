using System;
using Src.Map.Garrisons.FX;
using Src.SkinShop.Items.Base;
using Src.SkinShop.Skin;
using Src.SkinShop.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Levels.Level.UI
{
    public class LevelFinishedScreen : MonoBehaviour
    {
        [Header("Resources")]
        [SerializeField] private Sprite _completeFlag;
        [SerializeField] private Sprite _failFlag;
        
        [Header("Components")]
        [SerializeField] private Image _banner;
        [SerializeField] private AnimationTrigger _animationTrigger;
        [SerializeField] private TextMeshProUGUI _rewardText;
        [SerializeField] private Image _rewardIcon;
        [SerializeField] private DoubleRewardButton _doubleReward;

        public void Show(Level level)
        {
            if (level.IsControlledByPlayer)
            {
                ShowComplete(level);
            }
            else
            {
                ShowFail();
            }
        }

        private void ShowComplete(Level level)
        {
            _banner.sprite = _completeFlag;
            _rewardText.text =  $"+{level.Reward.Amount.ToString()}";
            _rewardIcon.gameObject.SetActive(true);
            Show();
            _animationTrigger.TriggerWin();
            _doubleReward.gameObject.SetActive(true);
            _doubleReward.BindLevelToRewardApplier(level);
        }

        private void ShowFail()
        {
            _banner.sprite = _failFlag;
            _rewardText.text = "";
            _rewardIcon.gameObject.SetActive(false);
            Show();
            _animationTrigger.TriggerHurt();
            _doubleReward.gameObject.SetActive(false);
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
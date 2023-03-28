using System;
using Src.Map.Fraction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Levels.Level.UI
{
    public class LevelFinishedScreen : MonoBehaviour
    {
        [Header("Resources")]
        [SerializeField] private Character _player;
        [SerializeField] private Sprite _completeFlag;
        [SerializeField] private Sprite _failFlag;
        [SerializeField] private Sprite _sadEyes;
        [SerializeField] private Sprite _happyEyes;
        
        [Header("Components")]
        [SerializeField] private Image _banner;
        [SerializeField] private Image _flag;
        [SerializeField] private Image _eyes;
        [SerializeField] private TextMeshProUGUI _rewardText;
        [SerializeField] private Image _rewardIcon;

        public void Show(Level level)
        {
            switch (level.Status)
            {
                case LevelCompletionState.Complete:
                    ShowComplete(level);
                    break;
                case LevelCompletionState.Incomplete:
                    ShowFail();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level.Status), level.Status, null);
            }
        }

        private void ShowComplete(Level level)
        {
            _banner.sprite = _completeFlag;
            _eyes.sprite = _happyEyes;
            _rewardText.text =  $"+{level.Reward.Amount.ToString()}";
            _rewardIcon.gameObject.SetActive(true);
            Show();
        }

        private void ShowFail()
        {
            _banner.sprite = _failFlag;
            _eyes.sprite = _sadEyes;
            _rewardText.text = "";
            _rewardIcon.gameObject.SetActive(false);
            Show();
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Start()
        {
            _flag.sprite = _player.Fraction.Flag;
        }
    }
}
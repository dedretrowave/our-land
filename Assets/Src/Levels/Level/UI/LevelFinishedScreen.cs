using Src.Fraction;
using UnityEngine;
using UnityEngine.Serialization;
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

        public void ShowComplete()
        {
            _banner.sprite = _completeFlag;
            _eyes.sprite = _happyEyes;
            Show();
        }

        public void ShowFail()
        {
            _banner.sprite = _failFlag;
            _eyes.sprite = _sadEyes;
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
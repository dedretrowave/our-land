using Src.Fraction;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Src.Levels.Level.UI
{
    public class LevelFinishedScreen : MonoBehaviour
    {
        [SerializeField] private Character _player;
        [SerializeField] private Sprite _completeFlag;
        [SerializeField] private Sprite _failFlag;
        
        [Header("Components")]
        [SerializeField] private Image _banner;
        [SerializeField] private Image _flag;

        public void ShowComplete()
        {
            _banner.sprite = _completeFlag;
            Show();
        }

        public void ShowFail()
        {
            _banner.sprite = _failFlag;
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
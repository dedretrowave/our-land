using System;
using Animations;
using Characters;
using Characters.Model;
using Characters.View;
using DI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Map.UI.Views
{
    public class LevelFinishedView : MonoBehaviour
    {
        [Header("Body")]
        [SerializeField] private GameObject _body;
        [SerializeField] private Button _closeButton;
        
        [Header("Banner Sprites")]
        [SerializeField] private Sprite _winSprite;
        [SerializeField] private Sprite _loseSprite;

        [Header("Character")]
        [SerializeField] private CharacterAnimations _animations;
        [SerializeField] private CharacterView _characterView;
        
        [Header("Banner")]
        [SerializeField] private Image _banner;

        [Header("Reward")]
        [SerializeField] private TextMeshProUGUI _rewardLabel;
        [SerializeField] private GameObject _rewardBody;
        [SerializeField] private Button _doubleRewardButton;

        public Action OnDoubleRewardApplied;

        public void ShowWin()
        {
            Open();
            
            _banner.sprite = _winSprite;
            _animations.PlayWin();
        }

        public void ShowLose()
        {
            Open();
            
            _banner.sprite = _loseSprite;
            _animations.PlayLose();
        }

        public void DisplayReward(int amount)
        {
            _rewardBody.SetActive(true);

            _rewardLabel.text = $"+{amount.ToString()}";
        }

        public void ApplyDoubleReward()
        {
            OnDoubleRewardApplied?.Invoke();
        }

        public void Open()
        {
            _rewardBody.SetActive(false);
            
            _body.SetActive(true);
        }

        private void Close()
        {
            _body.SetActive(false);
        }

        private void Awake()
        {
            _rewardBody.SetActive(false);
            _closeButton.onClick.AddListener(Close);
            _doubleRewardButton.onClick.AddListener(ApplyDoubleReward);
            
            DependencyContext.Dependencies.Add(new Dependency(typeof(LevelFinishedView), () => this));
        }

        private void Start()
        {
            CharacterContainer characterContainer = DependencyContext.Dependencies.Get<CharacterContainer>();
            CharacterModel player = characterContainer.GetByFraction(Fraction.Fraction.Player);
            
            _characterView.SetSkin(player.Skin);
        }
    }
}
using System;
using System.Collections;
using Animations;
using Characters.Base;
using Characters.Skins;
using Components;
using Components.Division;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Level.Region.Views
{
    public class RegionView : MonoBehaviour
    {
        private CharacterAnimations _animations;
        
        [Header("Components")]
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private Image _region;
        [SerializeField] private Image _flag;
        [SerializeField] private Image _eyes;
        [Header("Prefabs")]
        [SerializeField] private Division _division;

        [Header("Parameters")]
        [SerializeField] private float _garrisonIncreaseRate;
        [SerializeField] private int _garrisonInitialCount;
        [SerializeField] private float _divisionSpawnRate;

        public float DivisionSpawnRate => _divisionSpawnRate;
        public float GarrisonIncreaseRate => _garrisonIncreaseRate;
        public int GarrisonInitialCount => _garrisonInitialCount;

        public event Action<Division> OnDamageTaken;
        public event Action<Transform> OnGarrisonRelease;

        public void SetCount(int count)
        {
            _countText.text = count.ToString();
        }

        public void SetColor(Color color)
        {
            _region.color = color;
        }

        public void SetSkin(Skin skin)
        {
            _flag.sprite = skin.GetItemByType(SkinItemType.Flag).Sprite;
            _eyes.sprite = skin.GetItemByType(SkinItemType.Eyes).Sprite;
        }

        public void Release(Transform point)
        {
            OnGarrisonRelease?.Invoke(point);
        }

        public void SendDivision(RegionView target, Character owner)
        {
            Division division = Instantiate(_division, transform);
            division.Construct(owner, target);
        }

        public void PlayHurt()
        {
            _animations.PlayHurt();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Division division))
            {
                OnDamageTaken?.Invoke(division);
            }
        }

        private void Awake()
        {
            _animations = GetComponentInChildren<CharacterAnimations>();
        }
    }
}
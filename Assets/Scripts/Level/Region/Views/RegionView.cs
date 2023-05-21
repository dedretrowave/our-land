using System;
using Characters.Base;
using Characters.Skins;
using Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Level.Region.Views
{
    public class RegionView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private Image _region;
        [SerializeField] private Image _flag;
        [SerializeField] private Image _eyes;

        [Header("Parameters")]
        [SerializeField] private float _garrisonIncreaseRate;
        [SerializeField] private int _garrisonInitialCount;

        public float GarrisonIncreaseRate => _garrisonIncreaseRate;
        public int GarrisonInitialCount => _garrisonInitialCount;

        public event Action<Character> OnDamageTaken;
        public event Action OnGarrisonRelease;

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

        public void Release()
        {
            OnGarrisonRelease?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Division division))
            {
                OnDamageTaken?.Invoke(division.Owner);
            }
        }
    }
}
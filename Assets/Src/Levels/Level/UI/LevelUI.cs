using Src.Map.Fraction;
using Src.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Levels.Level.UI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private ColorsSettings _settings;

        public void Hide()
        {
            _image.gameObject.SetActive(false);
        }

        public void Show()
        {
            _image.gameObject.SetActive(true);
        }

        public void UpdateColorByLevel(Level level)
        {
            UpdateColor(level.Owner.Color);
        }

        public void UpdateColorByFraction(Map.Fraction.Fraction fraction)
        {
            UpdateColor(fraction.Color);
        }

        private void UpdateColor(Color color)
        {
            _image.color = color;
        }
    }
}
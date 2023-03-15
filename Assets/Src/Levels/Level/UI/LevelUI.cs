using Src.DI;
using Src.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Levels.Level.UI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private ColorsSettings _settings;

        private void Start()
        {
            _settings = DependencyContext.Dependencies.Get<ColorsSettings>();
        }

        public void UpdateColorByStatus(LevelCompletionState status)
        {
            _image.color = _settings.GetColorByLevelStatus(status);
        }
    }
}
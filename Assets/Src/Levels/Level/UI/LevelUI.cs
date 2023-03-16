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

        public void UpdateColorByStatus(LevelCompletionState status)
        {
            if (_settings == null) _settings = DependencyContext.Dependencies.Get<ColorsSettings>(); 
            
            _image.color = _settings.GetColorByLevelStatus(status);
        }
    }
}
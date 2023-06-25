using System;
using Newtonsoft.Json;

namespace Components.Music.Model
{
    [Serializable]
    public class MusicModel
    {
        private bool _isEnabled;

        public bool IsEnabled => _isEnabled;

        [JsonConstructor]
        public MusicModel(bool isEnabled)
        {
            _isEnabled = isEnabled;
        }

        public void SetEnabled(bool value)
        {
            _isEnabled = value;
        }
    }
}
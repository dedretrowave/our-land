using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using UnityEngine;
using Application = UnityEngine.Application;

namespace Src.Saves
{
    public class SaveFileHandler : MonoBehaviour
    {
        private string _serializedData;

        public T Load<T>(string path)
        {
#if UNITY_EDITOR
            _serializedData = GetSerializedInternal(path);
#else
            GetSerializedExternal(path);
#endif
            return JsonConvert.DeserializeObject<T>(_serializedData ?? "");
        }

        public void Save(string path, object data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

#if UNITY_EDITOR
            SaveInternal(path, json);
#else
            SaveExternal(path, json);
#endif
        }

        public void GetSerializedData(string data)
        {
            _serializedData = data;
        }

        private void SaveInternal(string path, string json)
        {
            File.WriteAllText(path, json);
        }

        [DllImport("__Internal")]
        private static extern void SaveExternal(string fieldName, string data);

        private string GetSerializedInternal(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
                return "";
            }
            
            return File.ReadAllText(path);
        }

        [DllImport("__Internal")]
        private static extern void GetSerializedExternal(string fieldName);

        private void Start()
        {
            DontDestroyOnLoad(this);
        }
    }
}
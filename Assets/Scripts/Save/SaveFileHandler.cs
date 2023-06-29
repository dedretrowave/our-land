using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using UnityEngine;
using Application = UnityEngine.Application;

namespace Save
{
    public class SaveFileHandler
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
            string json = JsonConvert.SerializeObject(
                data, 
                Formatting.Indented,
                new JsonSerializerSettings 
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });

#if UNITY_EDITOR
            SaveInternal(path, json);
#else
            SaveExternal(path, json);
#endif
        }

        private void GetSerializedData(string data)
        {
            _serializedData = data;
        }

        private void SaveInternal(string path, string json)
        {
            string filePath = $"{Application.persistentDataPath}/{path}.dat";

            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            try
            {
                File.WriteAllText(filePath, json);
            }
            catch (IOException e)
            {
                // ignore;
            }
        }

        [DllImport("__Internal")]
        private static extern void SaveExternal(string fieldName, string data);

        private string GetSerializedInternal(string path)
        {
            string filePath = $"{Application.persistentDataPath}/{path}.dat";
            
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
                return "";
            }
            
            return File.ReadAllText(filePath);
        }

        [DllImport("__Internal")]
        private static extern void GetSerializedExternal(string fieldName);
    }
}
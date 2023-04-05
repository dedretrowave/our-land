using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Application = UnityEngine.Application;

namespace Src.Saves
{
    public class SaveFileHandler
    {
        private string _pathToFile;

        public SaveFileHandler(string localFilePath)
        {
            _pathToFile = $"{Application.persistentDataPath}/{localFilePath}";
        }
        
        public T Load<T>()
        {
            if (!File.Exists(_pathToFile))
            {
                File.Create(_pathToFile);
                return JsonConvert.DeserializeObject<T>("");
            }

            string serializedData = File.ReadAllText(_pathToFile);
            return JsonConvert.DeserializeObject<T>(serializedData);
        }

        public void Save(object data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            File.WriteAllText(_pathToFile, json);
        }
    }
}
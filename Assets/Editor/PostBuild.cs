using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Src.Settings
{
    public class PostBuild
    {
        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget target, string targetPath)
        {
            if (target != BuildTarget.WebGL)
            {
                return;
            }

            var buildFolderPath = Path.Combine(targetPath, "Build");
            var info = new DirectoryInfo(buildFolderPath);
            var files = info.GetFiles("*.js");
            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];
                var filePath = file.FullName;
                var text = File.ReadAllText(filePath);
                text = text.Replace("UnityLoader.SystemInfo.mobile", "false");

                Debug.Log("Removing mobile warning from " + filePath);
                File.WriteAllText(filePath, text);
            }
        }
    }
}

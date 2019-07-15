using System.IO;
using Core.Utility;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Core.Editor
{
    public class IncrementBuildVersion : ScriptableObject
    {
        public static int currentBuild;
        public static string settingsPath;

        [MenuItem("Tools/Increment Build Number")]
        static void Init()
        {
            IncrementBuild();
        }

        [PostProcessBuild]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            IncrementBuild();
            Debug.Log(pathToBuiltProject);
        }

        private static void IncrementBuildNumber()
        {
            using (StreamReader sr = File.OpenText(settingsPath))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    currentBuild = int.Parse(s);
                }
            }
            currentBuild++;

            string appendText = $"{currentBuild}";
            File.WriteAllText(settingsPath, appendText);
        }

        private static void AssignSettingsPath()
        {
            settingsPath = Path.GetDirectoryName(Application.dataPath);
            settingsPath = Path.Combine(Application.dataPath, Version.BUILD_FILE);

            Debug.Log($"settingsPath: {settingsPath}");
        }

        private static void CreateBuildFile()
        {
            using (StreamWriter sw = File.CreateText(settingsPath))
            {
                sw.WriteLine("1");
            }
        }

        private static void IncrementBuild()
        {
            if (!File.Exists(settingsPath))
            {
                CreateBuildFile();
            }
            else
            {
                IncrementBuildNumber();
            }
            Debug.Log("Build version: " + currentBuild);
        }
    }
}
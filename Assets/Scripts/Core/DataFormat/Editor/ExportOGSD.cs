/*using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Core.DataFormat;
using UnityEditor;
using UnityEngine;

namespace Core.Editor
{
    public class ExportOGSD : EditorWindow
    {
        private string ogsdName;
        private string ogsdAuthor;
        private string ogsdDescription;
        private string ogsdParent;

        private string ogsdExportFile;
        private string editorActivatorFile;
        private List<ActionBase> m_ActionBases;

        private void OnEnable()
        {
            editorActivatorFile = Path.Combine(Application.dataPath, "Editor", "Activators.bin");
        }

        [MenuItem("Tools/Export OGSD")]
        private static void ShowWindow()
        {
            var window = GetWindow<ExportOGSD>();
            window.titleContent = new GUIContent("Export to OGSD");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Export to OGSD", EditorStyles.boldLabel);
            ogsdName = EditorGUILayout.TextField("File Name: ", ogsdName);
            ogsdAuthor = EditorGUILayout.TextField("Author: ", ogsdAuthor);
            ogsdDescription = EditorGUILayout.TextField("Description: ", ogsdDescription);
            ogsdParent = EditorGUILayout.TextField("Parent OGSD: ", ogsdParent);
            
            if (GUILayout.Button("Export"))
            {
                string finalPath = Path.Combine(Application.streamingAssetsPath, ogsdName + ".ogsd");
                if (File.Exists(finalPath))
                {
                    File.Delete(finalPath);
                }
                
                FileStream fileStream = File.Open(editorActivatorFile, FileMode.Open);
                var formatter = new BinaryFormatter();
                m_ActionBases = (List<ActionBase>) formatter.Deserialize(fileStream);
                fileStream.Close();
                
                FileStream ogsdStream = File.Create(finalPath);
                var ogsdFormatter = new BinaryFormatter();

                var ogsdFile = new OGSDFile();
                ogsdFile.dataHeader = new DataHeader
                {
                    version = 1.0f,
                    author = ogsdAuthor,
                    description = ogsdDescription,
                    parentDataFile = ogsdParent
                };
                    
                ogsdFile.actionBases = m_ActionBases;
                
                ogsdFormatter.Serialize(ogsdStream, ogsdFile);
                ogsdStream.Close();
            }
        }
    }
}*/
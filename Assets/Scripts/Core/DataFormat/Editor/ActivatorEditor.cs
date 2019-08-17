using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace Core.DataFormat.Editor
{
    public class ActivatorEditor : EditorWindow
    {
        #region Activator

        private string editorActivatorFile;
        public List<ActionBase> actionBases = new List<ActionBase>();
        private string activatorID;
        private Color32 activatorMarkerColour;
        private int activatorTab = 0;
        private int activatorBaseTab = 0;
        private string[] activatorStrings =
        {
            "Create",
            "List"
        };

        private string[] activatorBaseStrings =
        {
            "Traits",
            "Scripts"
        };

        public List<string> activatorScripts;

        #endregion

        private void OnDisable()
        {
            editorActivatorFile = Path.Combine(Application.dataPath, "Editor", "Activators.bin");
            FileStream fileStream = File.Create(editorActivatorFile);
            var formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, actionBases);
            fileStream.Close();
        }

        private void OnEnable()
        {
            actionBases.Clear();
            editorActivatorFile = Path.Combine(Application.dataPath, "Editor", "Activators.bin");
            FileStream fileStream = File.Open(editorActivatorFile, FileMode.Open);
            var formatter = new BinaryFormatter();
            actionBases = (List<ActionBase>) formatter.Deserialize(fileStream);
            fileStream.Close();
        }

        [MenuItem("Tools/Activator Editor")]
        private static void ShowWindow()
        {
            var window = GetWindow<ActivatorEditor>();
            window.titleContent = new GUIContent("Activator Editor");
            window.Show();
        }

        private void OnGUI()
        {
            if (actionBases == null)
            {
                actionBases = new List<ActionBase>();
            }
            ActivatorTab();
        }
        
        void ActivatorTab()
        {
            activatorTab = GUILayout.Toolbar(activatorTab, activatorStrings);
            switch (activatorTab)
            {
                case 0: // Create
                    ActivatorBase();
                    break;
                case 1:
                    ActivatorList();
                    break;
            }
        }

        void ActivatorBase()
        {
            activatorBaseTab = GUILayout.Toolbar(activatorBaseTab, activatorBaseStrings);
            switch (activatorBaseTab)
            {
                case 0:
                    ActivatorTraits();
                    break;
                case 1:
                    ActivatorScripts();
                    break;
            }

            ActivatorSaveLoad();
        }

        void ActivatorSaveLoad()
        {
            GUILayout.Label("Saving & Loading", EditorStyles.boldLabel);
            if (GUILayout.Button("Save Activator"))
            {
                ActionBase actionBase = new ActionBase
                {
                    ID = activatorID,
                    scripts = activatorScripts,
                    markerColour =
                    {
                        r = activatorMarkerColour.r, g = activatorMarkerColour.g, b = activatorMarkerColour.b, a = activatorMarkerColour.a
                    }
                };
                actionBases.Add(actionBase);
            }
        }

        void ActivatorTraits()
        {
            GUILayout.Label("Activator Traits", EditorStyles.boldLabel);
            activatorID = EditorGUILayout.TextField("ID", activatorID);
            activatorMarkerColour = EditorGUILayout.ColorField("Marker Colour", activatorMarkerColour);
        }

        void ActivatorScripts()
        {
            GUILayout.Label("Activator Scripts", EditorStyles.boldLabel);
            ScriptableObject target = this;
            SerializedObject so = new SerializedObject(target);
            SerializedProperty stringsProperty = so.FindProperty("activatorScripts");

            EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
            so.ApplyModifiedProperties();
        }

        void ActivatorList()
        {
            GUILayout.Label("List", EditorStyles.boldLabel);
            ScriptableObject target = this;
            SerializedObject so = new SerializedObject(target);
            SerializedProperty stringsProperty = so.FindProperty("actionBases");

            EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
            so.ApplyModifiedProperties();
        }
    }
}
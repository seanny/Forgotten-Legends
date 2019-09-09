using UnityEditor;
using UnityEngine;

namespace Core.World.Editor
{
    [InitializeOnLoad]
    public class MapEditor : EditorWindow
    {
        public Map map;

        static void Init()
        {
            MapEditor mapEditor = (MapEditor) EditorWindow.GetWindow(typeof(MapEditor));
            mapEditor.Show();
        }

        private void OnGUI()
        {
            if (map != null) 
            {
                SerializedObject serializedObject = new SerializedObject (this);
                SerializedProperty serializedProperty = serializedObject.FindProperty ("localizationData");
                EditorGUILayout.PropertyField (serializedProperty, true);
                serializedObject.ApplyModifiedProperties ();

                if (GUILayout.Button ("Export Map")) 
                {
                    Debug.Log($"Exporting Map...");
                }
            }
        }
    }
}
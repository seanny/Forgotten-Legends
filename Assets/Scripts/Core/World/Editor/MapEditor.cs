using System;
using UnityEditor;
using UnityEngine;

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

            if (GUILayout.Button ("Save data")) 
            {
                Debug.Log($"Save data");
            }
        }
    }
}
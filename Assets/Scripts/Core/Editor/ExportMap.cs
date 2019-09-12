using System;
using System.Collections.Generic;
using System.IO;
using Core.MathUtil;
using Core.Utility;
using UnityEditor;
using UnityEngine;
using Core.World;

namespace Core.Editor
{
    public class ExportMap : EditorWindow
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Map");
        public string mapID;
        public string mapWorldspace;
        public string mapName;
        
        [MenuItem("Tools/Export Scene as Map")]
        private static void ShowWindow()
        {
            var window = GetWindow<ExportMap>();
            window.titleContent = new GUIContent("Export Map");
            window.Show();
        }

        private void OnGUI()
        {
            mapID = EditorGUILayout.TextField("Map ID", mapID);
            mapWorldspace = EditorGUILayout.TextField("Worldspace", mapWorldspace);
            mapName = EditorGUILayout.TextField("Name", mapName);
            
            if (GUILayout.Button("Save Map"))
            {
                WriteMap();
            }
        }
        
        private void WriteMap()
        {
            Map map = new Map();
            map.mapHeader.mapName = mapName;
            map.mapHeader.mapWorldspace = mapWorldspace;
            map.mapHeader.mapID = mapID;
            map.mapObjects = new List<MapObject>();
            
            if (!mapID.EndsWith(".json"))
            {
                mapID += ".json";
            }
            
            GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();

            int count = 0;
            foreach (var item in gameObjects)
            {
                if (item.tag == "MapExport")
                {
                    if (item.name.Contains(" ("))
                    {
                        Debug.Log($"Removing number from name");
                        int pos = item.name.IndexOf(" (");
                        item.name = item.name.Substring(0, pos);
                    }
                    MapObject mapObject = new MapObject();
                    mapObject.objectBaseFile = $"{item.name}";
                    mapObject.objectName = item.name;
                    mapObject.objectPosition = new Vec3(item.transform.position);
                    mapObject.objectRotation = new Quat(item.transform.rotation);
                    mapObject.objectScale = new Vec3(item.transform.localScale);
                    ExportAttachedScript exportAttachedScript;
                    if (item.TryGetComponent<ExportAttachedScript>(out exportAttachedScript) == true)
                    {
                        mapObject.objectScripts = exportAttachedScript.luaScripts;
                    }
                    map.mapObjects.Add(mapObject);
                    count++;
                }
            }
            
            string jsonData = JsonUtility.ToJson(map);
            string finalPath = Path.Combine(path, mapID);
            File.WriteAllText(finalPath, jsonData);
            Debug.Log($"Exported {count} objects to \"{finalPath}\"");
        }
    }
}
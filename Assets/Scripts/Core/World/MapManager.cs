//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

public class MapManager : Singleton<MapManager>
{
    public List<Map> m_Maps;
    public List<GameObject> m_GameObjects;
    public Transform tracked;
    public Vector3 m_Size = new Vector3(80.0f, 20.0f, 80.0f);
    public bool isUpdating { get; private set; }
    
    NavMeshData m_NavMesh;
    AsyncOperation m_Operation;
    NavMeshDataInstance m_Instance;
    List<NavMeshBuildSource> m_Sources = new List<NavMeshBuildSource>();

    private void Start()
    {
        isUpdating = true;
        StartCoroutine(NavMeshUpdate());
    }

    private void OnEnable()
    {
        m_NavMesh = new NavMeshData();
        m_Instance = NavMesh.AddNavMeshData(m_NavMesh);
        if (tracked == null)
        {
            tracked = transform;
        }
        UpdateNavMesh();
    }

    private void OnDisable()
    {
        m_Instance.Remove();
    }

    private IEnumerator NavMeshUpdate()
    {
        while (true)
        {
            UpdateNavMesh();
            yield return m_Operation;
        }
    }
    
    void UpdateNavMesh()
    {
        isUpdating = true;
        NavMeshSourceTag.Collect(ref m_Sources);
        var defaultBuildSettings = NavMesh.GetSettingsByID(0);
        var bounds = QuantizedBounds();
        NavMeshBuilder.UpdateNavMeshData(m_NavMesh, defaultBuildSettings, m_Sources, bounds);
        isUpdating = false;
    }
    
    static Vector3 Quantize(Vector3 v, Vector3 quant)
    {
        float x = quant.x * Mathf.Floor(v.x / quant.x);
        float y = quant.y * Mathf.Floor(v.y / quant.y);
        float z = quant.z * Mathf.Floor(v.z / quant.z);
        return new Vector3(x, y, z);
    }
    
    Bounds QuantizedBounds()
    {
        // Quantize the bounds to update only when theres a 10% change in size
        var center = tracked ? tracked.position : transform.position;
        return new Bounds(Quantize(center, 0.1f * m_Size), m_Size);
    }

    public void RemoveAllMaps()
    {
        foreach (var item in m_GameObjects)
        {
            Destroy(item);
        }

        m_GameObjects.Clear();
        m_Maps.Clear();
    }
    
    public void LoadAllMaps(string worldspaceID)
    {
        isUpdating = true;
        // Search inside the StreamingAssets/Worldspace directory for all json files recursively.
        string[] jsonFiles = Directory.GetFiles(Path.Combine(Application.streamingAssetsPath, "Map"), "*.json", SearchOption.AllDirectories);
        foreach (var item in jsonFiles)
        {
            // We want to remove anything before the Map folder from the string
            string fileName = Path.GetFileName(item);

            // Parse the json data from the map file into a Map data type
            LoadMap(worldspaceID, fileName);
        }
    }

    private void LoadMap(string worldspaceID, string mapID)
    {
        isUpdating = true;
        // Append .json to worldspaceID if not already there.
        if (!mapID.EndsWith(".json"))
        {
            mapID += ".json";
        }
        
        // Read the worldspace json data from the worldspace definition json file. 
        string jsonData = AssetUtility.ReadAsset("Map", $"{mapID}");
        
        // Serialise the json data into a Worldspace data type.
        Map map = JsonUtility.FromJson<Map>(jsonData);
        
        // Check if the worldspace in the map is the same as the worldspaceID variable
        if (map.mapWorldspace == worldspaceID)
        {
            // Add the new map into the m_Maps list
            m_Maps.Add(map);
            CreateMapObjects(map);
        }
    }

    private void CreateMapObjects(Map map)
    {
        isUpdating = true;
        foreach (var item in map.mapObjects)
        {
            if (item.objectFile.StartsWith("Special::"))
            {
                if (CreateSpecialObject(item) == false)
                {
                    Debug.LogWarning($"Could not create special object, possible incorrect file name: {item.objectFile}");
                }
            }
        }
    }

    private bool CreateSpecialObject(ObjectItem objectItem)
    {
        isUpdating = true;
        GameObject _gameObject = null;
        switch (objectItem.objectFile)
        {
            case "Special::Plane":
                _gameObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
                break;
            case "Special::Cube":
                _gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                break;
            case "Special::Capsule":
                _gameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                break;
            case "Special::Cylinder":
                _gameObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                break;
            case "Special::Quad":
                _gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                break;
            case "Special::Sphere":
                _gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                break;
            default:
                Debug.LogWarning($"objectFile (currently \"{objectItem.objectFile}\") is not at a valid value.");
                break;
        }

        if (_gameObject != null)
        {
            _gameObject.transform.position = new Vector3(objectItem.objectPosition.x, objectItem.objectPosition.y, objectItem.objectPosition.z);
            _gameObject.transform.rotation = new Quaternion(objectItem.objectRotation.x, objectItem.objectRotation.y,
                objectItem.objectRotation.z, objectItem.objectRotation.w);
            _gameObject.transform.localScale = new Vector3(objectItem.objectScale.x, objectItem.objectScale.y, objectItem.objectScale.z);
            if (objectItem.objectWalkable == true)
            {
                _gameObject.AddComponent<NavMeshSourceTag>();
            }
            m_GameObjects.Add(_gameObject);
            return true;
        }
        return false;
    }
    
    void OnDrawGizmosSelected()
    {
        if (m_NavMesh)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(m_NavMesh.sourceBounds.center, m_NavMesh.sourceBounds.size);
        }

        Gizmos.color = Color.yellow;
        var bounds = QuantizedBounds();
        Gizmos.DrawWireCube(bounds.center, bounds.size);

        Gizmos.color = Color.green;
        var center = tracked ? tracked.position : transform.position;
        Gizmos.DrawWireCube(center, m_Size);
    }
}
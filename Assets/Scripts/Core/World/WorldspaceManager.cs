//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class WorldspaceManager : Singleton<WorldspaceManager>
{
    private List<Worldspace> m_Worldspaces;

    private void Start()
    {
        // Initialise the m_Worldspace object
        m_Worldspaces = new List<Worldspace>();
        
        // Load all worldspaces
        LoadAllWorldspaces();
        SetPlayerWorldspace("TestWorldspace");
    }

    public void LoadAllWorldspaces()
    {
        // Search inside the StreamingAssets/Worldspace directory for all json files recursively.
        string[] jsonFiles = Directory.GetFiles(Path.Combine(Application.streamingAssetsPath, "Worldspace"), "*.json", SearchOption.AllDirectories);
        foreach (var item in jsonFiles)
        {
            // We want to remove anything before the Worldspace folder from the string
            string fileName = Path.GetFileName(item);
            
            // Parse the json data from the worldspace file into a worldspace data type
            LoadWorldspace(fileName);
        }
    }

    public void LoadWorldspace(string worldspaceID)
    {
        // Append .json to worldspaceID if not already there.
        if (!worldspaceID.EndsWith(".json"))
        {
            worldspaceID += ".json";
        }
        
        // Read the worldspace json data from the worldspace definition json file. 
        string jsonData = AssetUtility.ReadAsset("Worldspace", $"{worldspaceID}");
        
        // Serialise the json data into a Worldspace data type.
        Worldspace worldspace = JsonUtility.FromJson<Worldspace>(jsonData);
        
        // Add the new worldspace into the m_Worldspaces list
        m_Worldspaces.Add(worldspace);
    }

    public void SetPlayerWorldspace(string worldspaceID)
    {
        SetActorWorldspace(PlayerManager.Instance.GetPlayer(), worldspaceID);
    }
    
    public void SetActorWorldspace(Actor actor, string worldspaceID)
    {
        actor.actorWorldspace = worldspaceID;
        RemoveAllMaps();
        LoadAllMaps(worldspaceID);
    }
    
    private void RemoveAllMaps()
    {
        MapManager.Instance.RemoveAllMaps();
    }

    private void LoadAllMaps(string worldspaceID)
    {
        MapManager.Instance.LoadAllMaps(worldspaceID);
    }
}
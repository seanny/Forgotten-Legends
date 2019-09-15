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
using System.Collections.Generic;
using System.IO;
using Core.Player;
using Core.Utility;
using UnityEngine;
using Core.CommandConsole;
using Core.Services;

namespace Core.World
{
    public class WorldspaceManager : Singleton<WorldspaceManager>
    {
        public List<Worldspace> m_Worldspaces { get; private set; }

        private void Start()
        {
            // Initialise the m_Worldspace object
            m_Worldspaces = new List<Worldspace>();
        
            // Load all worldspaces
            LoadAllWorldspaces();
        
            // Load the test worldspace to ensure that the worldspace system gets loaded.
            SetPlayerWorldspace("TestWorldspace");
            
            ServiceLocator.GetService<Ocean>().GenerateWater(500, 500);
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

        [RegisterCommand(Help = "SetPlayerWorldspace")]
        static void CommandSetPlayerWorldspace(CommandArg[] args)
        {
            bool found = false;
            string worldspaceName = args[0].String;
            foreach (var item in Instance.m_Worldspaces)
            {
                if (worldspaceName == item.worldspaceName)
                {
                    Instance.SetPlayerWorldspace(worldspaceName);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Terminal.print($"Cannot find worldspace: {worldspaceName}");
            }
            else
            {
                Terminal.print($"Worldspace set to {worldspaceName}");
            }
        }
        
        public void SetPlayerWorldspace(string worldspaceID)
        {
            SetActorWorldspace(ServiceLocator.GetService<PlayerManager>().GetPlayer(), worldspaceID);
        }
    
        public void SetActorWorldspace(Actor.Actor actor, string worldspaceID)
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
}
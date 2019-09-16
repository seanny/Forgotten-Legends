//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using System.Collections.Generic;
using System.IO;
using Core.Player;
using Core.Utility;
using UnityEngine;
using Core.CommandConsole;
using Core.Services;

namespace Core.World
{
    public class WorldspaceManager : IService
    {
        public List<Worldspace> worldspaces = new List<Worldspace>();

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
            worldspaces.Add(worldspace);
        }

        [RegisterCommand(Help = "SetPlayerWorldspace")]
        private static void CommandSetPlayerWorldspace(CommandArg[] args)
        {
            bool found = false;
            string worldspaceName = args[0].String;
            foreach (var item in ServiceLocator.GetService<WorldspaceManager>().worldspaces)
            {
                if (worldspaceName == item.worldspaceID)
                {
                    ServiceLocator.GetService<WorldspaceManager>().SetPlayerWorldspace(worldspaceName);
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
            // TODO: Show a loading screen prior to this
            SetActorWorldspace(ServiceLocator.GetService<PlayerManager>().GetPlayer(), worldspaceID);
            
            // TODO: Add a reference into the Worldspace files so that these can be modded easily.
            ServiceLocator.GetService<Ocean>().GenerateWater(500, 500);
            ServiceLocator.GetService<CloudManager>().ToggleCloudGeneration(true);
            
            RemoveAllMaps();
            LoadAllMaps(worldspaceID);
        }
    
        public void SetActorWorldspace(Actor.Actor actor, string worldspaceID)
        {
            actor.actorWorldspace = worldspaceID;
        }
    
        private void RemoveAllMaps()
        {
            MapManager.Instance.RemoveAllMaps();
        }

        private void LoadAllMaps(string worldspaceID)
        {
            MapManager.Instance.LoadAllMaps(worldspaceID);
        }

        public void OnStart()
        {
            // Load all worldspaces
            LoadAllWorldspaces();
        
            // Load the test worldspace to ensure that the worldspace system gets loaded.
            SetPlayerWorldspace("TestWorldspace");
        }

        public void OnEnd()
        {
            RemoveAllMaps();
        }
    }
}
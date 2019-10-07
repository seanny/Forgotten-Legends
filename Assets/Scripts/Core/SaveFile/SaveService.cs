using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Core.MathUtil;
using Core.NonPlayerChar;
using Core.Player;
using Core.Quests;
using Core.Scripting;
using Core.Services;
using Core.Settings;
using Core.Utility;
using UnityEngine;
using Object = UnityEngine.Object;
using Version = Core.Utility.Version;

namespace Core.SaveFile
{
    public class SaveService : IService
    {
        private string m_SaveName;
        private Player.Player m_Player = null;

        public void OnStart()
        {
            m_Player = ServiceLocator.GetService<PlayerManager>().GetPlayer();
            ServiceHelper.Instance.onUpdate += Update;
        }

        public void OnEnd()
        {
            ServiceHelper.Instance.onUpdate -= Update;
            m_Player = null;
        }
        
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Backslash))
            {
                // /// File Name: {CharacterName}-{SaveType}-{Num}.{FILE_EXT}
                m_SaveName =
                    $"{ServiceLocator.GetService<PlayerManager>().GetPlayer().name}-{SaveType.Quick.ToString()}.{SaveFile.FILE_EXT}";
                SaveGame(m_SaveName);
            }
        }

        public void SaveGame(string saveName)
        {
            if (!m_Player)
            {
                Logging.LogWarning($"Cannot save game, player is not assigned.");
                return;
            }


            SaveFile saveFile = new SaveFile();
            
            // Capture screenshot and save to the screenshot for later use. 
            Texture2D texture2D = ScreenCapture.CaptureScreenshotAsTexture();
            saveFile.screenshot = texture2D.EncodeToPNG();
            Object.Destroy(texture2D);

            // Assign current active quests to the save file active quests record. 
            saveFile.activeQuests = QuestJournal.Instance.activeQuests;
            
            // Assign completed active quests to the save file completed quests record.
            saveFile.completedQuests = QuestJournal.Instance.completedQuests;

            // Assign active scripts to the save file activeScripts record.
            saveFile.activeScripts = ScriptManager.Instance.m_LoadOrder;

            NPC[] npcs = GameObject.FindObjectsOfType<NPC>();

            NPCStats npcStats = new NPCStats();
            foreach (var npc in npcs)
            {
                npcStats.combatCurrentTime = npc.m_CombatScript.currentTime;
                npcStats.navMeshDest = new Vec3(npc.m_MovementScript.m_Destination);
                npcStats.navMeshMoving = npc.m_MovementScript.m_Moving;
                npcStats.hash = npc.actorHash;
                npcStats.name = npc.name;
                
                List<Actor.Actor> actorList = npc.m_SightScript.actorsInSight;
                List<string> actors = new List<string>();
                foreach (var actor in actorList)
                {
                    actors.Add(actor.actorHash);
                }
                npcStats.actorInSightHash = actors;
                npcStats.position = new Vec3(npc.transform.position);
                npcStats.rotation = new Quat(npc.transform.rotation);
                npcStats.currentWorldspace = npc.actorWorldspace;
                npcStats.playerFactions = npc.actorFaction.actorFactions;
                npcStats.playerInventory = npc.actorInventory.inventoryItems;
                npcStats.playerStats = npc.actorStatController.actorStats;
                npcStats.hungerCheckTime = 0.0f;
                saveFile.npcStats.Add(npcStats);
            }

            saveFile.playerStats.hash = m_Player.actorHash;
            saveFile.playerStats.name = m_Player.name;
            saveFile.playerStats.position = new Vec3(m_Player.transform.position);
            saveFile.playerStats.rotation = new Quat(m_Player.transform.rotation);
            saveFile.playerStats.currentWorldspace = m_Player.actorWorldspace;
            saveFile.playerStats.playerFactions = m_Player.actorFaction.actorFactions;
            saveFile.playerStats.playerInventory = m_Player.actorInventory.inventoryItems;
            saveFile.playerStats.playerStats = m_Player.actorStatController.actorStats;
            saveFile.playerStats.hungerCheckTime = m_Player.hungerCheckTime;
            
            // TODO: Count the total save hours.
            saveFile.saveHours = 0;

            // Assign the saveVersion to the current major number.
            saveFile.saveVersion = Version.major;

            // TODO: Add SetSaveBool(key, value) and GetSaveBool(key) to Lua API
            saveFile.scriptBooleans = null;
            
            // TODO: Add SetSaveFloat(key, value) and GetSaveFloat(key) to Lua API
            saveFile.scriptFloats = null;
            
            // TODO: Add SetSaveInt(key, value) and GetSaveInt(key) to Lua API
            saveFile.scriptIntegers = null;
            
            // TODO: Add SetSaveString(key, value) and GetSaveString(key) to Lua API
            saveFile.scriptStrings = null;
            
            FileStream fs = new FileStream(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", GameSettings.Instance.FolderName, "Saves", m_SaveName), FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try 
            {
                formatter.Serialize(fs, saveFile);
            }
            catch (SerializationException e) 
            {
                Logging.LogError("Failed to serialize. Reason: " + e.Message);
            }
            finally 
            {
                fs.Close();
            }
            
            Logging.Log($"Saved game to {m_SaveName}");

        }
    }
}
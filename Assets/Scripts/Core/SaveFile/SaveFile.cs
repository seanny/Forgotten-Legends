using System;
using System.Collections.Generic;
using System.Security.Principal;
using Core.NonPlayerChar;
using Core.Player;
using Core.Quests;
using Core.Weather;

namespace Core.SaveFile
{
    public enum SaveType
    {
        Auto,
        Quick,
        Manual
    }
    
    /// <summary>
    /// SaveFile
    /// File Name: {CharacterName}-{SaveType}-{Num}.{FILE_EXT}
    /// </summary>
    [Serializable]
    public class SaveFile
    {
        public static readonly string FILE_EXT = ".flsave";

        /// <summary>
        /// Static Version of the save, corresponds to game major version.
        /// </summary>
        public int saveVersion;
        
        /// <summary>
        /// DateTime of the save.
        /// </summary>
        public DateTime saveDateTime;

        /// <summary>
        /// Hours since the character was created.
        /// </summary>
        public int saveHours;

        /// <summary>
        /// Screenshot of the save.
        /// </summary>
        public byte[] screenshot;

        /// <summary>
        /// Stats of the player
        /// </summary>
        public PlayerStats playerStats;

        /// <summary>
        /// Stats of all NPCs
        /// </summary>
        public List<NPCStats> npcStats;

        /// <summary>
        /// Active Lua Scripts
        /// </summary>
        public List<string> activeScripts;

        /// <summary>
        /// Lua Booleans
        /// </summary>
        public Dictionary<string, bool> scriptBooleans;
        
        /// <summary>
        /// Lua Integers
        /// </summary>
        public Dictionary<string, int> scriptIntegers;
        
        /// <summary>
        /// Lua Floats
        /// </summary>
        public Dictionary<string, float> scriptFloats;
        
        /// <summary>
        /// Lua Strings
        /// </summary>
        public Dictionary<string, string> scriptStrings;

        /// <summary>
        /// Time of day for DayNightCycle
        /// </summary>
        public float currentTimeOfDay;

        /// <summary>
        /// Active Quests
        /// </summary>
        public Dictionary<Quest, string> activeQuests;
        
        /// <summary>
        /// Completed Quests
        /// </summary>
        public List<Quest> completedQuests;

        /// <summary>
        /// Current Weather as of the time of saving.
        /// </summary>
        public WeatherData weatherData;
    }
}
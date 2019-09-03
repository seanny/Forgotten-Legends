using System;
using Core.MathUtil;

namespace Core.DataFormat
{
    [Serializable]
    public class NPCBase : ScriptableRecord
    {
        /// <summary>
        /// Is Female (Male = False, Female = True)
        /// </summary>
        public bool isFemale;

        /// <summary>
        /// Is Protected (i.e. Cannot be killed by NPC's)
        /// </summary>
        public bool isProtected;

        /// <summary>
        /// Respawns the NPC when the map is unloaded.
        /// </summary>
        public bool respawn;

        /// <summary>
        /// Determines whether the NPC is unique
        /// </summary>
        public bool isUnique;

        /// <summary>
        /// Level multiplier
        /// If player is level 1 and NPC levelMult is 2 then NPC will be level 2
        /// </summary>
        public int levelMult;
        
        // TODO: Add SoundTrack record
        public string soundtrackID; 

        /// <summary>
        /// Is NPC invulnerable.
        /// If true, weapons do not affect the NPC (i.e. no damage, blood or sound effect)
        /// </summary>
        public bool isInvulnerable;

        /// <summary>
        /// NPC Alpha
        /// Useful for ghost characters.
        /// </summary>
        public float alpha;

        /// <summary>
        /// Determines the minimum health
        /// Actual health may vary but will not be less than healthOffset
        /// </summary>
        public float healthOffset;
        
        // Add faction records
        public string[] factions;

        // TODO: Add Race record
        public string raceID;
    }
}
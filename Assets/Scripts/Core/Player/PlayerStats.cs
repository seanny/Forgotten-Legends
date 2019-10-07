using System;
using System.Collections.Generic;
using Core.Factions;
using Core.Interactable;
using Core.MathUtil;
using Core.Stats;

namespace Core.Player
{
    [Serializable]
    public class PlayerStats
    {
        public string hash;
        public string name;
        public Vec3 position;
        public Quat rotation;
        public string currentWorldspace;
        public List<BaseStat> playerStats;
        public List<InteractableData> playerInventory;
        public List<Faction> playerFactions;
        public float hungerCheckTime;
    }
}
using System;
using System.Collections.Generic;
using Core.MathUtil;
using Core.Player;

namespace Core.NonPlayerChar
{
    [Serializable]
    public class NPCStats : PlayerStats
    {
        public int combatCurrentTime;
        public Vec3 navMeshDest;
        public bool navMeshMoving;
        public List<string> actorInSightHash;
    }
}
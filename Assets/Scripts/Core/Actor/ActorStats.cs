//
//  Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
//  This document is the property of Outlaw Games Studio.
//  It is considered confidential and proprietary.
//
//  This document may not be reproduced or transmitted in any form
//  without the consent of Outlaw Games Studio.
//

using System;
using System.Collections.Generic;

// Base Class for all characters, including NPC's and the player character
namespace Core.Actor
{
    [Serializable]
    public class ActorStats
    {
        // Actor Level. If maxLevel is 0, then they have no level cap.
        public int currentLevel;
        public int maxLevel;

        // Actor Stats
        public int strength;
        public int perception;
        public int endurance;
        public int speech;
        public int intelligence;
        public int sneak;
        public int luck;

        // Actor Gender
        public bool isFemale;

        // Protection Stats
        public bool isProtected;
        public bool isUnique;
        public bool isInvulnerable;
        
        // See-Thru
        public float alphaLevel;
        
        // Actor faction
        public List<string> factions;
    }
}

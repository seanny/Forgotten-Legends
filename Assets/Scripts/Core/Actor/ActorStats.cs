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

//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

namespace Core.Actor
{
    [System.Serializable]
    public class ActorClass
    {
        /* Class System, selected by the player on character creation.
         * Certain classes are also assigned to certain NPC's.
         * NPC: Class
         * Guards: Knight, Paladin, Fighter
         * Bandits: Barbarian, Thief, Rogue
         * Witches, Warlocks: Sorcerer, Wizard
         * Priests, Monks: Druid, Wizard
         * Hunter: Ranger
         * Vampire: Fighter, Barbarian, Sorcerer, Wizard, Ranger, Rogue, Thief
         * Adventurer: Fighter, Barbarian, Sorcerer, Wizard, Ranger
         * Demon: Fighter, Barbarian, Sorcerer, Wizard, Ranger, Rogue, 
         */
        public enum Class
        {
            // Common Classes
            Fighter,
            Barbarian,
            Paladin,
            Bard,
            Sorcerer,
            Wizard,
            Cleric,
            Druid,
            Ranger,
            Rogue,
            Thief
        }

        public Class currentClass;
    }
}

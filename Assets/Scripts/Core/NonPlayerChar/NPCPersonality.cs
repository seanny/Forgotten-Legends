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

namespace Core.NonPlayerChar
{
    [Serializable]
    public class NPCPersonality
    {
        #region Enums
        public enum Agression
        {
            // Does not attack, unless provoked
            Unagressive,

            // Attacks enemies (enemy factions) on sight
            Agressive,

            // Attacks enemies (enemy factions) and neutrals (no faction/non enemy factions) on sight (
            VeryAgreesive,

            // Attacks anyone on sight (even if in same faction)
            Frenzied
        }
        #endregion // Enums

        public Agression agression;
    }
}

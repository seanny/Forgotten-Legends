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
using System.Collections.Generic;

[Serializable]
public class Faction
{
    /// <summary>
    /// Identifier
    /// </summary>
    public int id;

    /// <summary>
    /// Name
    /// </summary>
    public string name;

    /// <summary>
    /// Enemy Factions
    /// </summary>
    public int[] enemyFactions;
}

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

[Serializable]
public class Quest
{
    public bool isActive;
    public int xpReward;
    public string questID;
    public string questName;
    public string questDescription;
    public string[] objectiveNames;
    public string[] objectiveDescriptions;
}

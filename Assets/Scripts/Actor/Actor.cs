//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//
using UnityEngine;
using System.Collections;

public abstract class Actor : MonoBehaviour
{
    public ActorStats m_ActorStats;
    public ActorClass m_ActorClass;
    public ActorHealth m_HealthScript;
}

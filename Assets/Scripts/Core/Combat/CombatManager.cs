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
using UnityEngine;

namespace Core.Combat
{
    public class CombatManager : Singleton<CombatManager>
    {
        public AudioClip attackDamageSFX;
        public AudioClip swordWithdraw;
        public AudioClip swordSwing;
    }
}
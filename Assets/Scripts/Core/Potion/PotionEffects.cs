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
using System;
using System.Collections;

public class PotionEffects : Singleton<PotionEffects>
{
    public void AddHealthEffect(int health, Actor actor)
    {
        actor.m_HealthScript.AddHealth(health);
    }

    public void AddMagicEffect(int magic, Actor actor)
    {
        throw new NotImplementedException("Magic Regen Effect not yet implemented");
    }

    public void AddStaminaEffect(int magic, Actor actor)
    {
        throw new NotImplementedException("Stamina Regen Effect not yet implemented");
    }
}

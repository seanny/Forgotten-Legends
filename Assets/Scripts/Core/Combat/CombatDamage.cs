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

namespace Core.Combat
{
    public static class CombatDamage
    {
        public static void ApplyDamage(Actor.Actor attacker, Actor.Actor victim, float baseDamage, bool powerAttack)
        {
            float damage = baseDamage + attacker.actorStatController.GetStat("strength").statValue;
            if (powerAttack)
            {
                damage += (baseDamage * 1.5f) 
                          + (attacker.actorStatController.GetStat("luck").statValue / 2)
                          + (attacker.actorStatController.level.statValue / 5);
            }
            else
            {
                damage += (attacker.actorStatController.GetStat("luck").statValue / 2)
                          + (attacker.actorStatController.level.statValue / 5);
            }
            victim.m_HealthScript.TakeHealth(Mathf.FloorToInt(damage));
            attacker.animationController.SwordAttack(false);
        }
    }
}
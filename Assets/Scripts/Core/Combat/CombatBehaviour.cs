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
using System.Runtime.CompilerServices;
using UnityEngine;
using Core.Actor;
using Core.Inventory;
using Core.Player;

namespace Core.Combat
{
    public class CombatBehaviour : MonoBehaviour
    {
        public Actor.Actor m_AssignedActor;
        public float attackCooldown;
        public float nextAttackCooldown;

        private bool m_PowerAttack;
        
        private void Start()
        {
            InitActor();
        }

        private void InitActor()
        {
            if (m_AssignedActor == null)
            {
                m_AssignedActor = GetComponent<Actor.Actor>();
            }
        }

        public void InitiateAttack(bool powerAttack)
        {
            InitActor();
            if (attackCooldown > 0)
            {
                return;
            }
            if (nextAttackCooldown > 0)
            {
                return;
            }

            Debug.Log($"{m_AssignedActor.gameObject.name} is initiating an attack move.");
            m_PowerAttack = powerAttack;
            m_AssignedActor.animationController.SwordAttack(true);
            attackCooldown = 5f;
            nextAttackCooldown = 5f;
        }

        private void Update()
        {
            if (attackCooldown > 0.0f)
            {
                attackCooldown -= Time.deltaTime;
            }
            if (nextAttackCooldown > 0.0f)
            {
                nextAttackCooldown -= Time.deltaTime;
                if (nextAttackCooldown <= 0)
                {
                    m_AssignedActor.animationController.SwordAttack(false);
                }
            }
        }

        public void ApplyDamage(Actor.Actor actor, float baseDamage, bool powerAttack)
        {
            InitActor();
            float damage = baseDamage + m_AssignedActor.m_ActorStats.strength;
            if (powerAttack)
            {
                damage += (baseDamage * 1.5f) 
                        + (m_AssignedActor.m_ActorStats.luck / 2)
                        + (m_AssignedActor.m_ActorStats.currentLevel / 5);
            }
            else
            {
                damage += (m_AssignedActor.m_ActorStats.luck / 2)
                    + (m_AssignedActor.m_ActorStats.currentLevel / 5);
            }
            actor.m_HealthScript.TakeHealth(Mathf.FloorToInt(damage));
            m_AssignedActor.animationController.SwordAttack(false);
        }
    }
}
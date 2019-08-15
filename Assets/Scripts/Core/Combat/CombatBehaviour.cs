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
        public MeleeWeaponAttackVector m_MeleeWeaponAttackVector;
        
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

            if (m_MeleeWeaponAttackVector == null)
            {
                m_MeleeWeaponAttackVector = GetComponentInChildren<MeleeWeaponAttackVector>();
            }
        }

        public void InitiateAttack(bool powerAttack)
        {
            InitActor();
            if (attackCooldown > 0)
            {
                return;
            }

            Debug.Log($"{m_AssignedActor.gameObject.name} is initiating an attack move.");
            m_PowerAttack = powerAttack;
            AudioSource.PlayClipAtPoint(CombatManager.Instance.swordSwing, m_AssignedActor.gameObject.transform.position);
            m_AssignedActor.animationController.SwordAttack(true);
            attackCooldown = 1.15f;
        }

        private void Update()
        {
            if (attackCooldown > 0.0f)
            {
                attackCooldown -= Time.deltaTime;
                if (attackCooldown > 0.0f && attackCooldown <= 0.1f)
                {
                    m_MeleeWeaponAttackVector.EnableCollider();                    
                }
                if (attackCooldown <= 0)
                {
                    m_AssignedActor.animationController.SwordAttack(false);
                    m_MeleeWeaponAttackVector.DisableCollider();
                }
            }
        }
    }
}
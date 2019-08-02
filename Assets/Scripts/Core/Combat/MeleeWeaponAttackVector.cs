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
using Core.Actor;

namespace Core.Combat
{
    public class MeleeWeaponAttackVector : MonoBehaviour
    {
        [SerializeField]
        private Actor.Actor m_Actor;

        private BoxCollider m_Collider;

        private void Start()
        {
            m_Actor = GetComponentInParent<Actor.Actor>();
            m_Collider = GetComponent<BoxCollider>();
            m_Collider.enabled = false;
        }

        public void EnableCollider()
        {
            m_Collider.enabled = true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Actor.Actor>() != null)
            {
                Actor.Actor victim = other.GetComponent<Actor.Actor>();
                CombatDamage.ApplyDamage(m_Actor, victim, 5.0f, false);
                AudioSource.PlayClipAtPoint(CombatManager.Instance.attackDamageSFX, victim.gameObject.transform.position);
                m_Collider.enabled = false;
            }
        }
    }
}
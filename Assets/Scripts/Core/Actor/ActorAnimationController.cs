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

namespace Core.Actor
{
    public class ActorAnimationController : MonoBehaviour
    {
        public Animator m_Animator;
        public bool hasWeapon = true;

        private void Start()
        {
            m_Animator = GetComponent<Animator>();
            SetIdle();
        }
        
        public void SetSwim(bool toggle)
        {
            StartIdle();
            m_Animator.SetBool("Swimming", toggle);
        }
        
        public void SetIdle()
        {
            StartIdle();
            m_Animator.SetBool("SwordDraw", false);
        }

        public void IdleToWalking()
        {
            if (!hasWeapon)
            {
                m_Animator.SetInteger("Walking", 1);
            }
            else
            {
                m_Animator.SetBool("SwordWalk", true);
            }
        }

        public void StartWalking()
        {
            if (!hasWeapon)
            {
                m_Animator.SetInteger("Walking", 1);
                m_Animator.SetInteger("Running", 0);
            }
            else
            {
                m_Animator.SetBool("SwordWalk", true);
                m_Animator.SetBool("SwordRun", false);
            }
        }
        
        public void StartIdle()
        {
            if (!hasWeapon)
            {
                m_Animator.SetInteger("Walking", 0);
                m_Animator.SetInteger("Running", 0);
            }
            else
            {
                m_Animator.SetBool("SwordWalk", false);
                m_Animator.SetBool("SwordRun", false);
            }
        }
        
        public void StartRunning()
        {
            if (!hasWeapon)
            {
                m_Animator.SetInteger("Walking", 0);
                m_Animator.SetInteger("Running", 1);
            }
            else
            {
                m_Animator.SetBool("SwordWalk", false);
                m_Animator.SetBool("SwordRun", true);
            }
        }

        public void StartFalling()
        {
            m_Animator.SetInteger("Falling", 1);
        }
        
        public void StopFalling()
        {
            m_Animator.SetInteger("Falling", 0);
            m_Animator.SetInteger("Landing", 1);
        }
        
        public void StartJump()
        {
            if (!hasWeapon)
            {
                m_Animator.SetInteger("Jumping", 1);
            }
            else
            {
                m_Animator.SetBool("SwordIdleJump", true);
            }
        }

        public void SwordDraw()
        {
            StartIdle();
            m_Animator.SetBool("SwordDraw", true);
        }

        public void SwordAttack(bool attack)
        {
            StartIdle();
            if (!hasWeapon)
            {
                m_Animator.SetBool("SwordAttack", attack);
            }
            else
            {
                m_Animator.SetBool("SwordIdleAttack", attack);
            }
        }
    }
}
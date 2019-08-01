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
        private bool m_IsWalking;
        private bool m_IsRunning;
        
        private void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_Animator.Play("Idle");
        }

        public void IdleToWalking()
        {
            m_Animator.SetInteger("Walking", 1);
        }

        public void StartWalking()
        {
            m_Animator.SetInteger("Walking", 1);
            m_Animator.SetInteger("Running", 0);
        }
        
        public void StartIdle()
        {
            m_Animator.SetInteger("Walking", 0);
            m_Animator.SetInteger("Running", 0);
            m_Animator.SetInteger("Jumping", 0);
            m_Animator.SetInteger("Falling", 0);
        }
        
        public void StartRunning()
        {
            m_Animator.SetInteger("Walking", 0);
            m_Animator.SetInteger("Running", 1);
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
            m_Animator.SetInteger("Jumping", 1);
        }
    }
}
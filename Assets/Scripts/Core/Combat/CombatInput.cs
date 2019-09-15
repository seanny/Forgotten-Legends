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
using Core.Services;
using UnityEngine;

namespace Core.Combat
{
    public class CombatInput : MonoBehaviour, IService
    {
        public int attackButton;
        
        private float m_PowerAttackTime;
        private CombatBehaviour m_CombatBehaviour;

        private void Start()
        {
            ResetPowerAttackTime();
            attackButton = 0;
            m_CombatBehaviour = GetComponent<CombatBehaviour>();
            ServiceLocator.AddService(this);
        }

        private void ResetPowerAttackTime()
        {
            m_PowerAttackTime = 0.0f;
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(attackButton))
            {
                m_PowerAttackTime += Time.deltaTime;
                m_CombatBehaviour.InitiateAttack(true);
            }

            if (Input.GetMouseButtonUp(attackButton) && m_PowerAttackTime < 1.0f)
            {
                ResetPowerAttackTime();

                m_CombatBehaviour.InitiateAttack(false);
                // TODO: Initiate regular attack
            }

            if (m_PowerAttackTime > 1.0f)
            {
                // TODO: Initiate power attack
            }
        }

        public void OnStart()
        {
            
        }

        public void OnEnd()
        {
            
        }
    }
}
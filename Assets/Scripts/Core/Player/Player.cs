//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using Core.Actor;
using Core.Hunger;
using UnityEngine;

namespace Core.Player
{
    public class Player : Actor.Actor
    {
        private HungerController m_HungerController;
        private float HungerCheckTime = 0f;
        private const int HUNGER_CHECK_MINUTES = 5;
        
        // Use this for initialization
        protected override void Start()
        {
            m_HealthScript = GetComponent<PlayerHealth>();
            if (!m_HealthScript)
            {
                m_HealthScript = gameObject.AddComponent<ActorHealth>();
            }

            m_HungerController = gameObject.AddComponent<HungerController>();
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {
            HungerCheckTime += Time.deltaTime;
            if (HungerCheckTime > (60.0f * HUNGER_CHECK_MINUTES) && m_HungerController.HungerLevel < 1)
            {
                // Drain the players health every 5 minutes by 10 points if they are hungry.
                m_HealthScript.TakeHealth(10);
            }
        }
    }
}

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

namespace Core.Player
{
    public class Player : Actor.Actor
    {
        // Use this for initialization
        protected override void Start()
        {
            m_HealthScript = GetComponent<PlayerHealth>();
            if (!m_HealthScript)
            {
                m_HealthScript = gameObject.AddComponent<ActorHealth>();
            }
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

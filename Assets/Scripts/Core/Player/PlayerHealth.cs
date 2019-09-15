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
using Core.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Player
{
    public class PlayerHealth : ActorHealth, IService
    {
        [SerializeField] private Scrollbar m_Scrollbar;

        protected override void Start()
        {
            base.Start();
            ServiceLocator.AddService(this);
        }

        private void LateUpdate()
        {
            if (m_Scrollbar == null)
            {
                return;
            }
            
            // Make sure that we cast currentHealth as a float otherwise C# will floor it for some reason.
            float healthPoints = (float)currentHealth / maxHealth;
            GetScrollbar().size = healthPoints;
        }

        public Scrollbar GetScrollbar()
        {
            if (m_Scrollbar == null)
            {
                m_Scrollbar = GameObject.FindWithTag("HealthBar").GetComponent<Scrollbar>();
            }
            Debug.Log($"m_Scrollbar = {m_Scrollbar}");
            
            return m_Scrollbar;
        }

        public void OnStart() { }

        public void OnEnd()
        {
            m_Scrollbar = null;
        }
    }
}

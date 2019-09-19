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
using System.Diagnostics.Contracts;
using Core.Player;
using Core.Services;
using Core.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UserInterface
{
    public class StatsBar : MonoBehaviour, IService
    {
        private const float WAIT_TIME = 1.0f;
        private float m_Time;
        [SerializeField] private GameObject m_StatsBar;
        private bool isShown;

        public void ToggleStatsBar(bool toggle)
        {
            m_StatsBar.SetActive(toggle);
        }

        private void Start()
        {
            m_StatsBar = GameObject.FindWithTag("StatsBar");
            ServiceLocator.AddService(this);
        }

        private void Update()
        {
            m_Time += Time.deltaTime;

            if (m_Time > WAIT_TIME)
            {
                m_Time -= WAIT_TIME;
                
                if (
                    ServiceLocator.GetService<PlayerManager>().GetPlayer().m_HealthScript.currentHealth >= ServiceLocator.GetService<PlayerManager>().GetPlayer().m_HealthScript.maxHealth
                )
                {
                    isShown = false;
                }
                else
                {
                    isShown = true;
                }
                ImageUtils.FadeAlpha(ServiceLocator.GetService<PlayerHealth>().GetScrollbar(), isShown == true ? 1.0f : 0.0f, 1.0f);
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
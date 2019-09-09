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
using System.Collections;
using Core.Misc;
using UnityEngine;

namespace Core.World
{
    public class TimedObject : MonoBehaviour
    {
        public int activationTime;
        public int deactivationTime;
        private Renderer m_Renderer;

        private void Start()
        {
            m_Renderer = GetComponent<Renderer>();
            StartCoroutine(OnTimedObjectUpdate());
        }

        IEnumerator OnTimedObjectUpdate()
        {
            while (true)
            {
                if (activationTime >= DayNightCycle.Instance.currentTimeOfDay/24
                    && deactivationTime <= DayNightCycle.Instance.currentTimeOfDay/24)
                {
                    m_Renderer.enabled = true;
                }
                if (deactivationTime >= DayNightCycle.Instance.currentTimeOfDay/24
                    && activationTime <= DayNightCycle.Instance.currentTimeOfDay/24)
                {
                    m_Renderer.enabled = false;
                }
                yield return new WaitForSeconds(1);
            }
        }
    }
}
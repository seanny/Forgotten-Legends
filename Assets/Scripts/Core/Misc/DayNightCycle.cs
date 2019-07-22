//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using UnityEngine;
using Core.Settings;

namespace Core.Misc
{
    public class DayNightCycle : MonoBehaviour
    {
        public Light sun;
        public float secondsInFullDay = 120f;
        [Range(0, 1)]
        public float currentTimeOfDay = 0;
        [HideInInspector]
        public float timeMultiplier = 1f;

        float sunInitialIntensity;
        private bool m_MoonUpdated;

        void Start()
        {
            GameSettings.Instance.SetProperty("TestProp", "TestValue");
            sunInitialIntensity = sun.intensity;
        }

        void Update()
        {
            UpdateSun();

            currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

            if (currentTimeOfDay >= .5f && m_MoonUpdated == false)
            {
                m_MoonUpdated = true;
                MoonController.Instance.IncrementMoonStage(); 
            }
            
            if (currentTimeOfDay >= 1)
            {
                m_MoonUpdated = false;
                currentTimeOfDay = 0;
            }
        }

        void UpdateSun()
        {
            sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

            float intensityMultiplier = 1;
            if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
            {
                intensityMultiplier = 0;
            }
            else if (currentTimeOfDay <= 0.25f)
            {
                intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
            }
            else if (currentTimeOfDay >= 0.73f)
            {
                intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
            }

            sun.intensity = sunInitialIntensity * intensityMultiplier;
        }
    }
}

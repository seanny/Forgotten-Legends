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

namespace Core.Debugging
{
    public class FPSCounter : Singleton<FPSCounter>
    {
        float deltaTime = 0.0f;
        float m_FPS, m_MS;
#if UNITY_DEVELOPMENT || UNITY_EDITOR
        public bool showFPSCounter = true;
#else
    public bool showFPSCounter = false;
#endif

        // Update is called once per frame
        private void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        }

        private void LateUpdate()
        {
            m_MS = Mathf.FloorToInt(deltaTime * 1000.0f);
            m_FPS = Mathf.FloorToInt(1.0f / deltaTime);
        }

        private void OnGUI()
        {
            if (!showFPSCounter)
                return;
            
            int w = Screen.width, h = Screen.height;
            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = Color.white;
            string text = $"{m_FPS} FPS ({m_MS} ms)";
            GUI.Label(rect, text, style);
        }
    }
}

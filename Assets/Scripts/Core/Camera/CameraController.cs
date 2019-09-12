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
using Core.Utility;

namespace Core.Camera
{
    [RequireComponent(typeof(CameraRotation))]
    [RequireComponent(typeof(CameraScrolling))]
    public class CameraController : Singleton<CameraController>
    {
        #region Variables

        // Private Variables
        public Transform lookAt;
        public Transform cameraTransform { get; private set; }
        
        public bool freeCamera { get; private set; }
        
        #endregion

        private void Start()
        {
            MouseCursor.LockCursor(true);
            cameraTransform = transform;
        }

        public void ToggleFreeCamera(bool toggle)
        {
            freeCamera = toggle;
        }
    }
}

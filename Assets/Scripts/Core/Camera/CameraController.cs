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
        #endregion

        private void Start()
        {
            //MouseCursor.LockCursor(true);
            cameraTransform = transform;
        }
    }
}

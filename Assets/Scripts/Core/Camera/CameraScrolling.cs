//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using Core.Services;
using UnityEngine;

namespace Core.Camera
{
    public class CameraScrolling : Singleton<CameraScrolling>
    {
        public float currentScroll = 0.0f;
        private const float SCROLL_MIN = 5.0f;
        private const float SCROLL_MAX = 15.0f;
        public bool rotationLocked { get; private set; }

        public void ToggleScrolling(bool toggle)
        {
            rotationLocked = toggle;
        }

        // Update is called once per frame
        private void Update()
        {
            if(rotationLocked == false)
            {
                if (ServiceLocator.GetService<CameraController>().freeCamera == false)
                {
                    currentScroll += Input.GetAxis("Mouse ScrollWheel");
                    currentScroll = Mathf.Clamp(currentScroll, SCROLL_MIN, SCROLL_MAX);
                }
            }
        }

        private void LateUpdate()
        {
            if (ServiceLocator.GetService<CameraController>().freeCamera == false)
            {
                ServiceLocator.GetService<CameraController>().cameraTransform.position = ServiceLocator.GetService<CameraController>().lookAt.position - ServiceLocator.GetService<CameraController>().cameraTransform.forward * currentScroll;
                ServiceLocator.GetService<CameraController>().cameraTransform.position = ServiceLocator.GetService<CameraController>().lookAt.position - ServiceLocator.GetService<CameraController>().cameraTransform.forward * currentScroll;
                ServiceLocator.GetService<CameraController>().cameraTransform.LookAt(ServiceLocator.GetService<CameraController>().lookAt.position);
            }
        }
    }
}

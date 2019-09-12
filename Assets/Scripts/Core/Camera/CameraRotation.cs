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
    public class CameraRotation : Singleton<CameraRotation>
    {
        private const float Y_ANGLE_MIN = -50f;
        private const float Y_ANGLE_MAX = 50f;

        public float currentX { get; private set; }
        public float currentY { get; private set; }
        public Vector3 currentRot { get; private set; }
        public bool rotationLocked { get; private set; }

        private float rotationSmoothTime = .12f;
        private Vector3 rotationSmoothVelocity;

        public void ToggleRotation(bool toggle)
        {
            rotationLocked = toggle;
        }

        // Update is called once per frame
        private void Update()
        {
            if (!rotationLocked)
            {
                if (CameraController.Instance.freeCamera == false)
                {
                    GetCameraRotation();
                    ClampCameraRotation();
                    SmoothCamera();
                    RotateCamera();
                }
            }
        }

        private void GetCameraRotation()
        {
            // Set the current camera position
            currentX += Input.GetAxis("Mouse X");
            currentY += Input.GetAxis("Mouse Y");
        }

        private void ClampCameraRotation()
        {
            // Prevent the position from going above constant floats.
            // We should also have an X angle min/max so as to prevent the player from going 360 degrees over and over
            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        }

        private void SmoothCamera()
        {
            currentRot = Vector3.SmoothDamp(currentRot, new Vector3(currentY, currentX), ref rotationSmoothVelocity, rotationSmoothTime);
        }

        private void RotateCamera()
        {
            CameraController.Instance.cameraTransform.eulerAngles = currentRot;
        }
    }
}

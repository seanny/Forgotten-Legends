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
using System.Collections;

public class CameraRotation : MonoBehaviour
{
    private const float Y_ANGLE_MIN = -50f;
    private const float Y_ANGLE_MAX = 50f;

    public float currentX { get; private set; }
    public float currentY { get; private set; }
    public Vector3 currentRot { get; private set; }

    private float rotationSmoothTime = .12f;
    private Vector3 rotationSmoothVelocity;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Set the current camera position
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        // Prevent the position from going above constant floats.
        // We should also have an X angle min/max so as to prevent the player from going 360d over and over
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        currentRot = Vector3.SmoothDamp(currentRot, new Vector3(currentY, currentX), ref rotationSmoothVelocity, rotationSmoothTime);
        CameraController.Instance.cameraTransform.eulerAngles = currentRot;
    }
}

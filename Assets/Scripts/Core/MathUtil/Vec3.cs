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
using UnityEngine;

namespace Core.MathUtil
{
    [Serializable]
    public class Vec3
    {
        public float x { get; private set; }
        public float y { get; private set; }
        public float z { get; private set; }
        
        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public Vec3(Vector3 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
            this.z = vector.z;
        }
    }
}
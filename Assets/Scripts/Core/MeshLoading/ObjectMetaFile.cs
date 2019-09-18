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
using Core.MathUtil;

namespace Core.MeshLoading
{
    [Serializable]
    public class ObjectMetaFile
    {
        public string meshObj;
        public string meshLODNear;
        public string meshLODFar;
        public string texturePng;
        public string normalPng;
        public string ambientOcclusionPng;
        public string heightMapPng;
        public bool isWalkable;
        public MetaLight light;
        public MetaInteractable interactable;
        public MetaRigidbody rigidbody;
        public MetaCollider collider;
        public MetaTimed timed;
        public float drawDistance;
        public string objectName;
        public string bookID;
    }

    [Serializable]
    public class MetaLight
    {
        public bool isEnabled;
        public bool isSpotlight;
        public float range;
        public float angle;
        public float intensity;
        public Colour colour;
    }
    
    [Serializable]
    public class MetaRigidbody
    {
        public bool isEnabled;
        public Vec3 position;
        public Quat rotation;
        public float mass;
        public float drag;
    }

    [Serializable]
    public class MetaCollider
    {
        public bool isEnabled;
        public bool isTrigger;
    }

    [Serializable]
    public class MetaTimed
    {
        public bool isEnabled;
        public int activationHour;
        public int deactivationHour;
    }

    [Serializable]
    public class MetaInteractable
    {
        public bool isInteractable;
        public int interactableType;
    }
}
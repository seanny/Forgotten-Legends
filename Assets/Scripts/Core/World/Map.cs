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

namespace Core.World
{
    /// <summary>
    /// Defines objects that can be placed in a specific worldspace.
    /// </summary>
    [Serializable]
    public class Map
    {
        /// <summary>
        /// The ID for the world.
        /// </summary>
        public string mapID;
    
        /// <summary>
        /// The worldspace that the map is assigned to. The map will thus only show up if the player is in the specified worldspace.
        /// </summary>
        public string mapWorldspace;
    
        /// <summary>
        /// The name for the map that will show up int the User Interface.
        /// </summary>
        public string mapName;
    
        /// <summary>
        /// Map objects that will be used to define the object file, position, rotation and scale.
        /// </summary>
        public ObjectItem[] mapObjects;
    }

    [Serializable]
    public class ObjectItem
    {
        public string objectFile;
        public ObjectVector objectPosition;
        public ObjectQuarternion objectRotation;
        public ObjectVector objectScale;
        public bool objectWalkable;
        public ObjectLight objectLight;
        public ObjectInteractable objectInteractable;
        public bool objectCollision;
        public bool objectRigidbody;
    }

    [Serializable]
    public class ObjectLight
    {
        public bool isEnabled;
        public bool isSpotLight;
        public float lightRange;
        public float lightAngle;
        public float lightIntensity;
        public ObjectColor lightColour;
    }

    [Serializable]
    public class ObjectVector
    {
        public float x;
        public float y;
        public float z;
    }

    [Serializable]
    public class ObjectColor
    {
        public string r;
        public string g;
        public string b;
    }

    [Serializable]
    public class ObjectQuarternion
    {
        public float x;
        public float y;
        public float z;
        public float w;
    }

    [Serializable]
    public class ObjectInteractable
    {
        public bool isInteractable;
        public int interactionType;
    }
}
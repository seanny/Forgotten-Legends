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
using System.Collections.Generic;
using Core.MathUtil;

namespace Core.World
{
    /// <summary>
    /// Defines objects that can be placed in a specific worldspace.
    /// </summary>
    [Serializable]
    public class Map
    {
        /// <summary>
        /// Map Header that will be used to define the map meta data such as name and worldspace.
        /// </summary>
        public MapHeader mapHeader = new MapHeader();
        
        /// <summary>
        /// Map objects that will be used to define the object file, position, rotation and scale.
        /// </summary>
        public List<MapObject> mapObjects;

        /*
        /// <summary>
        /// Map NPCs that will be used to define the NPC base ID and position.
        /// </summary>
        public MapNPC[] mapNpcs;
        */
    }

    [Serializable]
    public class MapHeader
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
    }
    
    [Serializable]
    public class MapObject
    {
        public string objectBaseFile;
        public string objectName;
        public Vec3 objectPosition;
        public Quat objectRotation;
        public Vec3 objectScale;
        public List<string> objectScripts;
    }

    [Serializable]
    public class MapNPC
    {
        public string npcID;
        public Vec3 npcPosition;
        public List<string> npcScripts;
    }
}
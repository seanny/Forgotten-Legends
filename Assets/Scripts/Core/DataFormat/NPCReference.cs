using System;
using Core.MathUtil;

namespace Core.DataFormat
{
    [Serializable]
    public class NPCReference : Record
    {
        /// <summary>
        /// Action Base Reference
        /// </summary>
        public NPCBase npcBase;
        
        /// <summary>
        /// Action 3D Position
        /// </summary>
        public Vec3 npcPosition;
    }
}
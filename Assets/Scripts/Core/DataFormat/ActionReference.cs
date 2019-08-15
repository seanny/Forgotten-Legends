using System;
using Core.MathUtil;

namespace Core.DataFormat
{
    [Serializable]
    public class ActionReference : Record
    {
        /// <summary>
        /// Action Base Reference
        /// </summary>
        public ActionBase actionBase;
        
        /// <summary>
        /// Action 3D Position
        /// </summary>
        public Vec3 actionPosition;
    }
}
using System;
using Core.MathUtil;

namespace Core.DataFormat
{
    [Serializable]
    public class ActionBase : Record
    {
        /// <summary>
        /// Lua Script File Path
        /// </summary>
        public string script;

        /// <summary>
        /// Gizmo Colour
        /// </summary>
        public Colour markerColour;
    }
}
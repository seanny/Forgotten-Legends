using System;
using Core.MathUtil;

namespace Core.DataFormat
{
    [Serializable]
    public class ActionBase : ScriptableRecord
    {
        /// <summary>
        /// Gizmo Colour
        /// </summary>
        public Colour markerColour = new Colour();
    }
}
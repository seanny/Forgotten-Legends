using System;
using System.Collections.Generic;
using Core.MathUtil;

namespace Core.DataFormat
{
    /// <summary>
    /// Scriptable Record is a Record that can hold one or more Lua scripts.
    /// </summary>
    [Serializable]
    public class ScriptableRecord : Record
    {
        /// <summary>
        /// Path to Lua scripts
        /// </summary>
        public List<string> scripts;
    }
}
using System;

namespace Core.DataFormat
{
    /// <summary>
    /// Parent Class for all record types
    /// </summary>
    [Serializable]
    public class Record
    {
        /// <summary>
        /// Unique ID for the record.
        /// If there is more than 1 record with the same ID, the first found record will be used.
        /// </summary>
        public string ID;
    }
}
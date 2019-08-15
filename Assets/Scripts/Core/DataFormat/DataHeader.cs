using System;

namespace Core.DataFormat
{
    [Serializable]
    public class DataHeader
    {
        public float version;
        public string author;
        public string description;
        public string parentDataFile;
    }
}
using System;
using System.Collections.Generic;

namespace Core.DataFormat
{
    [Serializable]
    public class OGSDFile
    {
        public DataHeader dataHeader;
        public List<ActionBase> actionBaseList;
    }
}
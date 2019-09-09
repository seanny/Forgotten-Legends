using System.Collections.Generic;
using UnityEngine;

namespace Core.World
{
    [ExecuteInEditMode]
    public class ExportAttachedScript : MonoBehaviour
    {
        #if UNITY_EDITOR
        public List<string> luaScripts;
        #endif
    }
}
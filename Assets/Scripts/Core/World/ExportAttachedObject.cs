using System.Collections.Generic;
using UnityEngine;

namespace Core.World
{
    [ExecuteInEditMode]
    public class ExportAttachedObject : MonoBehaviour
    {
#if UNITY_EDITOR
        public string objectID;
        public string bookID;
#endif
    }
}
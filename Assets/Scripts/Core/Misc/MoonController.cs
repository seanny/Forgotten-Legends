using System;
using UnityEngine;

namespace Core.Misc
{
    public class MoonController : MonoBehaviour
    {
        public float distance = 1000.0f;
        public float scale = 15.0f;
        
        private void Start()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.x, distance);
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
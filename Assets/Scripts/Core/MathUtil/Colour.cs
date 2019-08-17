using System;
using UnityEngine;

namespace Core.MathUtil
{
    [Serializable]
    public class Colour
    {
        public Colour()
        {
            
        }
        
        public Colour(Color32 colour)
        {
            r = colour.r;
            g = colour.g;
            b = colour.b;
            a = colour.a;
        }

        public byte r;
        public byte g;
        public byte b;
        public byte a;
    }
}
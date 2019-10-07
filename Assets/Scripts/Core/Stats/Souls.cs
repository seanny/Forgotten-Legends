using System;
using Core.CommandConsole;

namespace Core.Stats
{
    [Serializable]
    public class Souls : BaseStat
    {
        public static int SoulsCollected { get; private set; }

        public Souls()
        {
            statID = "souls";
            Reset();
        }

        /// <summary>
        /// Add a single soul to the players soul count and also increment the global SoulsCollected amount. 
        /// </summary>
        public void AddSoul()
        {
            statValue++;
            Souls.SoulsCollected++;
        }

        public override void Reset()
        {
            statValue = 0;
        }
    }
}
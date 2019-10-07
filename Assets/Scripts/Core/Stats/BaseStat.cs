using System;
using UnityEngine;

namespace Core.Stats
{
    [Serializable]
    public class BaseStat
    {
        public string statID { get; protected set; }
        public int statValue;

        /// <summary>
        /// Called when the player gains more xp than the required (i.e. levels up).
        /// </summary>
        /// <param name="amount"></param>
        public virtual void LevelUp(int amount)
        {
            statValue += amount;
        }

        /// <summary>
        /// Resets all statistics
        /// </summary>
        public virtual void Reset()
        {
            statValue = 0;
        }
    }
}
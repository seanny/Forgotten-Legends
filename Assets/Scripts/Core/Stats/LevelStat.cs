using System;
using Core.Utility;
using UnityEngine;

namespace Core.Stats
{
    [Serializable]
    public class LevelStat : BaseStat
    {
        private int MIN_LEVEL = 1;

        public LevelStat()
        {
            statID = "current_level";
            statValue = MIN_LEVEL;
        }
        
        public override void LevelUp(int amount)
        {
            statValue += amount;
            Logging.Log($"{statID} has been leveled up by {amount} to {statValue}");
        }

        public override void Reset()
        {
            statValue = MIN_LEVEL;
        }
    }
}
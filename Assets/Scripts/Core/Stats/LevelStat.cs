using Core.Utility;
using UnityEngine;

namespace Core.Stats
{
    public class LevelStat : BaseStat
    {
        private int MIN_LEVEL = 1;

        private void Start()
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
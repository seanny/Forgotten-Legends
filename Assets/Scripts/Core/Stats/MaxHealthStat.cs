using Core.Utility;
using UnityEngine;

namespace Core.Stats
{
    public class MaxHealthStat : BaseStat
    {
        private const int MIN_VALUE = 100;

        public MaxHealthStat()
        {
            statID = "max_health";
            statValue = MIN_VALUE;
        }

        public override void LevelUp(int amount)
        {
            int levelAmount = Mathf.FloorToInt(amount * (10 / 25));
            if (levelAmount < 1)
            {
                levelAmount = 1;
            }

            if (levelAmount > MIN_VALUE)
            {
                levelAmount = MIN_VALUE;
            }
            
            statValue += levelAmount;
            Logging.Log($"{statID} has been leveled up by {levelAmount} to {statValue}");
        }

        public override void Reset()
        {
            statValue = MIN_VALUE;
        }
    }
}
using System;

namespace Core.Stats
{
    [Serializable]
    public class CurrentHealth : BaseStat
    {
        private int DEFAULT_HEALTH = 100;
        
        public CurrentHealth()
        {
            statID = "current_health";
            statValue = DEFAULT_HEALTH;
        }

        public override void LevelUp(int amount)
        {
            statValue = amount;
        }

        public override void Reset()
        {
            statValue = DEFAULT_HEALTH;
        }
    }
}
namespace Core.Stats
{
    public class CurrentHealth : BaseStat
    {
        private int DEFAULT_HEALTH = 100;
        
        private void Start()
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
namespace Core.Stats
{
    public class Thirst : BaseStat
    {
        private int MAX_VALUE = 100;

        public Thirst()
        {
            statID = "thirst";
        }
        
        public override void LevelUp(int amount)
        {
            // Reset the players hunger level on level up.
            Reset();
        }

        /// <summary>
        /// Increase the players thirst level.
        /// </summary>
        /// <param name="amount"></param>
        public void SatiateThirst(int amount)
        {
            statValue += amount;
            if (statValue > MAX_VALUE)
            {
                Reset();
            }
        }

        public override void Reset()
        {
            statValue = MAX_VALUE;
        }
    }
}
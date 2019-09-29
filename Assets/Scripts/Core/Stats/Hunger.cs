namespace Core.Stats
{
    public class Hunger : BaseStat
    {
        private int MAX_VALUE = 100;

        public override void LevelUp(int amount)
        {
            // Reset the players hunger level on level up.
            Reset();
        }

        public void SatiateHunger(int amount)
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
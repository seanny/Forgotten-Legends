namespace Core.Stats
{
    public class Endurance : BaseStat
    {
        private int MIN_VALUE = 1;
        
        public Endurance()
        {
            statID = "endurance";
            Reset();
        }

        public override void Reset()
        {
            statValue = MIN_VALUE;
        }
    }
}
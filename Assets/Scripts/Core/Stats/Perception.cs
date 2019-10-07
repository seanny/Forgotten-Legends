namespace Core.Stats
{
    public class Perception : BaseStat
    {
        private int MIN_VALUE = 1;
        
        public Perception()
        {
            statID = "perception";
            Reset();
        }

        public override void Reset()
        {
            statValue = MIN_VALUE;
        }
    }
}
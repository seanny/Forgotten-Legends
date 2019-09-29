namespace Core.Stats
{
    public class Perception : BaseStat
    {
        private int MIN_VALUE = 1;
        
        private void Start()
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
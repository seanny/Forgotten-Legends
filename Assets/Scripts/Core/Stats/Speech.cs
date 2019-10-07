namespace Core.Stats
{
    public class Speech : BaseStat
    {
        private int MIN_VALUE = 1;
        
        public Speech()
        {
            statID = "speech";
            Reset();
        }

        public override void Reset()
        {
            statValue = MIN_VALUE;
        }
    }
}
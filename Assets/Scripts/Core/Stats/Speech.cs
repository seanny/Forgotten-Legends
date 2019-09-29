namespace Core.Stats
{
    public class Speech : BaseStat
    {
        private int MIN_VALUE = 1;
        
        private void Start()
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
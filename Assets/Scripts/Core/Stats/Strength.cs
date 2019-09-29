namespace Core.Stats
{
    public class Strength : BaseStat
    {
        private int MIN_VALUE = 1;
        
        private void Start()
        {
            statID = "strength";
            Reset();
        }

        public override void Reset()
        {
            statValue = MIN_VALUE;
        }
    }
}
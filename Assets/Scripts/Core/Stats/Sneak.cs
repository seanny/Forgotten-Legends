namespace Core.Stats
{
    public class Sneak : BaseStat
    {
        private int MIN_VALUE = 1;

        private void Start()
        {
            statID = "sneak";
            Reset();
        }

        public override void Reset()
        {
            statValue = MIN_VALUE;
        }
    }
}
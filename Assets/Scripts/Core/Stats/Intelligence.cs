namespace Core.Stats
{
    public class Intelligence : BaseStat
    {
        private int MIN_VALUE = 1;

        public Intelligence()
        {
            statID = "intelligence";
            Reset();
        }

        public override void Reset()
        {
            statValue = MIN_VALUE;
        }
    }
}
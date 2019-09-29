namespace Core.Stats
{
    public class Luck : BaseStat
    {
        private int MIN_VALUE = 1;

        private void Start()
        {
            statID = "luck";
            Reset();
        }

        public override void Reset()
        {
            statValue = MIN_VALUE;
        }
    }
}
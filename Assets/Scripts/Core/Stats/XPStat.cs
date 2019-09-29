namespace Core.Stats
{
    public class XPStat : BaseStat
    {
        public bool canLevelUp { get; private set; }
        
        private void Start()
        {
            statID = "current_health";
            Reset();
        }

        public override void LevelUp(int amount)
        {
            statValue += amount;
        }

        public override void Reset()
        {
            statValue = 0;
        }
    }
}
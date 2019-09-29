namespace Core.Stats
{
    public class XPStat : BaseStat
    {
        public bool canLevelUp { get; private set; }
        
        private void Start()
        {
            statID = "xp";
            Reset();
        }

        public void SetLevelUp(bool toggle)
        {
            canLevelUp = toggle;
        }

        public override void Reset()
        {
            statValue = 0;
        }
    }
}
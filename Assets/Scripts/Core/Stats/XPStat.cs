namespace Core.Stats
{
    public class XPStat : BaseStat
    {
        public bool canLevelUp { get; private set; }
        
        public XPStat()
        {
            statID = "xp";
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
using System;

namespace Core.Stats
{
    [Serializable]
    public class Strength : BaseStat
    {
        private int MIN_VALUE = 1;
        
        public Strength()
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
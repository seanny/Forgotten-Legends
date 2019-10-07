using System;

namespace Core.Stats
{
    [Serializable]
    public class Sneak : BaseStat
    {
        private int MIN_VALUE = 1;

        public Sneak()
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
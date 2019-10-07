using System;

namespace Core.Stats
{
    [Serializable]
    public class Luck : BaseStat
    {
        private int MIN_VALUE = 1;

        public Luck()
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
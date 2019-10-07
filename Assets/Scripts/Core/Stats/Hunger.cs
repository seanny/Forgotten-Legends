using System;
using System.Timers;

namespace Core.Stats
{
    public class Hunger : BaseStat
    {
        private int MAX_VALUE = 100;
        private Timer m_Timer;
        
        public Hunger()
        {
            statID = "hunger";
            m_Timer = new Timer(60 * 5 * 100);
            m_Timer.Elapsed += OnHungerTimer;
            m_Timer.AutoReset = true;
            m_Timer.Enabled = true;
        }

        ~Hunger()
        {
            m_Timer.Stop();
            m_Timer.Dispose();
        }
        
        private void OnHungerTimer(object sender, ElapsedEventArgs e)
        {
            if (statValue > 0)
            {
                statValue--;
            }
        }

        public override void LevelUp(int amount)
        {
            // Reset the players hunger level on level up.
            Reset();
        }

        public void SatiateHunger(int amount)
        {
            statValue += amount;
            if (statValue > MAX_VALUE)
            {
                Reset();
            }
        }

        public override void Reset()
        {
            statValue = MAX_VALUE;
        }
    }
}
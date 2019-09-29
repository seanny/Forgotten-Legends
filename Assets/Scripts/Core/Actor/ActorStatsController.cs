using System.Collections.Generic;
using Core.Stats;
using UnityEngine;

namespace Core.Actor
{
    public class ActorStatsController : MonoBehaviour
    {
        private LevelStat m_Level;
        private CurrentHealth m_Health;
        private MaxHealthStat m_MaxHealth;
        private XPStat m_XP;
        private List<BaseStat> m_ActorStats;
        private int m_RequiredXP;

        public void GiveXP(int xp)
        {
            m_XP.LevelUp(xp);
            if (xp >= m_RequiredXP)
            {
                LevelUp();
                m_RequiredXP = m_Level.statValue * (100 + m_Level.statValue);
            }
        }

        protected void Start()
        {
            m_ActorStats = new List<BaseStat>();
            m_Level = GetComponent<LevelStat>();
            m_Health = GetComponent<CurrentHealth>();
            m_MaxHealth = GetComponent<MaxHealthStat>();
            BaseStat[] baseStats = GetComponents<BaseStat>();
            foreach (var baseStat in baseStats)
            {
                if (baseStat == m_Level 
                    || baseStat == m_Health 
                    || baseStat == m_MaxHealth)
                {
                    continue;
                }
                m_ActorStats.Add(baseStat);
            }
        }

        protected virtual void LevelUp()
        {
            m_Level.LevelUp(1);
            m_Health.LevelUp(m_MaxHealth.statValue);
            foreach (var stats in m_ActorStats)
            {
                stats.LevelUp(m_Level.statValue);
            }
        }
    }
}
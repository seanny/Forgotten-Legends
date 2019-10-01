using System.Collections.Generic;
using Core.Stats;
using UnityEngine;

namespace Core.Actor
{
    public class ActorStatsController : MonoBehaviour
    {
        public LevelStat level { get; private set; }
        public CurrentHealth health { get; private set; }
        public MaxHealthStat maxHealth { get; private set; }
        public XPStat xp { get; private set; }
        public List<BaseStat> actorStats { get; private set; }
        protected int m_RequiredXP;
        protected int m_LevelUpAdditionalXP = 100;

        protected void AssignRequiredXP()
        {
            m_RequiredXP = level.statValue * (m_LevelUpAdditionalXP + level.statValue);
        }

        protected void Start()
        {
            actorStats = new List<BaseStat>();
            level = GetComponent<LevelStat>();
            health = GetComponent<CurrentHealth>();
            maxHealth = GetComponent<MaxHealthStat>();
            BaseStat[] baseStats = GetComponents<BaseStat>();
            foreach (var baseStat in baseStats)
            {
                if (baseStat == level 
                    || baseStat == health 
                    || baseStat == maxHealth)
                {
                    continue;
                }
                actorStats.Add(baseStat);
            }

            int levelUpXP = int.Parse(Settings.GameSettings.Instance.GetProperty("iLevelUpAdditionalXP"));
            if (levelUpXP > 0)
            {
                m_LevelUpAdditionalXP = levelUpXP;
            }
            AssignRequiredXP();
        }

        public BaseStat GetStat(string statID)
        {
            foreach (var actorStat in actorStats)
            {
                if (actorStat.statID == statID)
                {
                    return actorStat;
                }
            }
            return null;
        }

        protected virtual void LevelUp()
        {
            level.LevelUp(1);
            health.LevelUp(maxHealth.statValue);
        }
    }
}
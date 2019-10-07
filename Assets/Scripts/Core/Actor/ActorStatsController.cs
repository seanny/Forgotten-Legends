using System;
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
        public List<BaseStat> actorStats;
        protected int m_RequiredXP;
        protected int m_LevelUpAdditionalXP = 100;

        protected void AssignRequiredXP()
        {
            m_RequiredXP = level.statValue * (m_LevelUpAdditionalXP + level.statValue);
        }

        protected void Start()
        {
            actorStats = new List<BaseStat>();
            level = new LevelStat();
            actorStats.Add(level);
            
            health = new CurrentHealth();
            actorStats.Add(health);
            
            maxHealth = new MaxHealthStat();
            actorStats.Add(maxHealth);
            
            xp = new XPStat();
            actorStats.Add(xp);

            int.TryParse(Settings.GameSettings.Instance.GetProperty("iLevelUpAdditionalXP"), out int levelUpXP);
            if (levelUpXP > 0)
            {
                m_LevelUpAdditionalXP = levelUpXP;
            }
            AssignRequiredXP();
        }

        /// <summary>
        /// Returns the BaseStat of a type, if none exists, create one and return that.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public BaseStat GetStat<T>()
        {
            foreach (var actorStat in actorStats)
            {
                if (actorStat.GetType() == typeof(T))
                {
                    return actorStat;
                }
            }
            BaseStat baseStat = (BaseStat) Activator.CreateInstance(typeof(T));
            return baseStat;
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
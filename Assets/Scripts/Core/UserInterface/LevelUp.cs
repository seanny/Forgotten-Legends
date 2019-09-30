using System;
using Core.Player;
using Core.Services;
using UnityEngine;

namespace Core.UserInterface
{
    public class LevelUp : MonoBehaviour
    {
        private const float LEVEL_UP_TIME = 5f;
        
        private float m_LevelUpTime = 0f;
        private bool m_LevelUpShown = false;

        private GameObject m_LevelUpUI;
        
        private void Start()
        {
            ToggleLevelUp(false);
        }

        private void Update()
        {
            if (!m_LevelUpShown)
            {
                return;
            }

            m_LevelUpTime += Time.deltaTime;
            if (m_LevelUpTime >= LEVEL_UP_TIME)
            {
                ToggleLevelUp(false);
            }
        }

        private void ToggleLevelUp(bool toggle)
        {
            m_LevelUpShown = toggle;
            m_LevelUpTime = 0;
        }
        
        public void ShowLevelUp()
        {
            m_LevelUpShown = true;
            m_LevelUpTime = 0;
        }
    }
}
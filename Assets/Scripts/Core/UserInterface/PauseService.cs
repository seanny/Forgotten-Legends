using System;
using Core.Minimap;
using UnityEngine;
using Core.Services;
using Core.Utility;

namespace Core.UserInterface
{
    public class PauseService : MonoBehaviour, IService
    {
        [SerializeField]
        private GameObject m_PauseMenu;

        public void OnStart()
        {
            
        }

        public void OnEnd()
        {
            
        }

        private void Start()
        {
            ServiceLocator.AddService(this);
            m_PauseMenu = GameObject.Find("PauseMenu");
            m_PauseMenu.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (m_PauseMenu.activeSelf)
                {
                    m_PauseMenu.SetActive(false);
                    MouseCursor.ToggleCursor(false);
                    MouseCursor.LockCursor(false);
                    ServiceLocator.GetService<MiniMap>().ToggleMinimap(true);
                    ServiceLocator.GetService<StatsBar>().ToggleStatsBar(true);
                }
                else
                {
                    MouseCursor.ToggleCursor(true);
                    m_PauseMenu.SetActive(true);
                    MouseCursor.LockCursor(true);
                    ServiceLocator.GetService<StatsBar>().ToggleStatsBar(false);
                }
            }
        }
    }
}
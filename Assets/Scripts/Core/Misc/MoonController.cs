using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Core.Misc
{
    public class MoonController : Singleton<MoonController>
    {
        /*
         * 0. Full Moon
         * 1. Waning Out
         * 2. Half Moon
         * 3. New Moon
         * 4. Half Moon
         * 5. Waning In
         */
        private int m_MoonStage;
        private Renderer m_Renderer;
        
        public Texture2D[] moonTextures;
        public Texture2D moonTexture;
        private float m_Distance = 1000.0f;
        private float m_Scale = 15.0f;

        private void Start()
        {
            InitRenderer();
            InitPosition();
            InitMoonStage();
        }

        private void InitRenderer()
        {
            m_Renderer = GetComponent<Renderer>();
        }
        
        private void InitPosition()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.x, m_Distance);
            transform.localScale = new Vector3(m_Scale, m_Scale, m_Scale);
        }

        private void InitMoonStage()
        {
            m_MoonStage = 0;
            m_Renderer.material.mainTexture = moonTextures[0];
        }

        public void IncrementMoonStage()
        {
            if (moonTextures.Length < 1)
            {
                Debug.LogWarning($"Moon stages textures not assigned, skipping.");
                return;
            }
            
            m_MoonStage++;
            if (m_MoonStage >= moonTextures.Length)
            {
                m_MoonStage = 0;
            }
            
            moonTexture = moonTextures[m_MoonStage];
            m_Renderer.material.SetTexture("_BaseMap", moonTexture);
        }
    }
}
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Core.Services;
using UnityEngine.Experimental.PlayerLoop;

namespace Core.UserInterface
{
    public class LoadingScreenService : IService
    {
        private Image m_LoadingScreen;
        private List<Texture2D> m_Textures = new List<Texture2D>();
        private const int WAIT_TIME = 1;
        private float m_WaitTime = 0.0f;
        private bool m_Waiting = false; 
        
        public void OnStart()
        {
            m_LoadingScreen = GameObject.FindWithTag("LoadingScreenTexture").GetComponent<Image>();
            m_LoadingScreen.gameObject.SetActive(false);
            
            string folderPath = Path.Combine(Application.streamingAssetsPath, "Textures", "Loading");
            string[] files = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);
            foreach (var textureFile in files)
            {
                m_Textures.Add(Utility.ImageUtils.LoadPNG(textureFile));
            }

            ServiceHelper.Instance.onUpdate += Update;
        }

        public void ToggleLoadingScreen(bool toggle)
        {
            if (toggle)
            {
                int index = Random.Range(0, m_Textures.Count - 1);
                m_LoadingScreen.gameObject.SetActive(true);
                m_LoadingScreen.sprite = Sprite.Create(m_Textures[index], new Rect(0, 0, m_Textures[index].width, m_Textures[index].height), new Vector2(0.5f, 0.5f));
            }
            else
            {
                m_Waiting = true;
                m_WaitTime = 0.0f;
                Utility.ImageUtils.FadeAlpha(m_LoadingScreen, 0.0f, WAIT_TIME);
            }
        }
        
        public void OnEnd()
        {
            
        }

        private void Update()
        {
            m_WaitTime += Time.deltaTime;
            if (m_Waiting && m_WaitTime >= WAIT_TIME)
            {
                m_LoadingScreen.gameObject.SetActive(false);
            }
        }
    }
}
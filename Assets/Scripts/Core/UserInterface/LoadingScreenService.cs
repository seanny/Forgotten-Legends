using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Core.Services;

namespace Core.UserInterface
{
    public class LoadingScreenService : IService
    {
        private Image m_LoadingScreen;
        private List<Texture2D> m_Textures = new List<Texture2D>();
        
        public void OnStart()
        {
            m_LoadingScreen = GameObject.FindWithTag("LoadingScreenTexture").GetComponent<Image>();
            m_LoadingScreen.gameObject.SetActive(false);
            
            string folderPath = Path.Combine(Application.streamingAssetsPath, "Textures", "Loading");
            string[] files = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);
            foreach (var textureFile in files)
            {
                Debug.Log($"textureFile: {textureFile}");
                m_Textures.Add(Utility.ImageUtils.LoadPNG(textureFile));
            }
        }

        public void ToggleLoadingScreen(bool toggle)
        {
            if (toggle)
            {
                int index = Random.Range(0, m_Textures.Count - 1);
                m_LoadingScreen.sprite = Sprite.Create(m_Textures[index], new Rect(0, 0, m_Textures[index].width, m_Textures[index].height), new Vector2(0.5f, 0.5f));
            }
            m_LoadingScreen.gameObject.SetActive(false);
        }
        
        public void OnEnd()
        {
            
        }
    }
}
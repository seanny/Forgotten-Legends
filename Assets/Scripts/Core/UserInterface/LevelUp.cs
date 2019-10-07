using Core.Localisation;
using Core.Services;
using Core.Settings;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

namespace Core.UserInterface
{
    public class LevelUp : MonoBehaviour, IService
    {
        [SerializeField] private Image m_LevelUpUI;
        [SerializeField] private Button m_LevelUpOKButton;
        [SerializeField] private TextMeshProUGUI m_Text;
        [SerializeField] private AudioSource m_LevelUpAudioSource;

        private int m_LocalisationKeys = 5;

        private void Start()
        {
            m_LevelUpUI = GetComponentInChildren<Image>();
            m_LevelUpOKButton = m_LevelUpUI.GetComponentInChildren<Button>();
            m_LevelUpAudioSource = GetComponentInChildren<AudioSource>();
            m_Text = m_LevelUpOKButton.GetComponentInChildren<TextMeshProUGUI>();
            ToggleLevelUp(false);
            int.TryParse(GameSettings.Instance.GetProperty("iLevelUpLocalisationKeys"), out m_LocalisationKeys);
        }

        private void ToggleLevelUp(bool toggle)
        {
            m_LevelUpUI.gameObject.SetActive(toggle);
        }
        
        public void ShowLevelUp()
        {
            // Get a random string from the locale file and then add 1 as the locale goes from 1 to 5, not 0 to 4.
            int index = Random.Range(0, m_LocalisationKeys) + 1;
            
            // Add a leading 0 if the index is below 10. I.e. 1 becomes 01, etc. 10 stays as 10.
            string num = index < 10 ? $"0{index}" : index.ToString();

            // Get the correct localisation string and return it.
            m_Text.text = ServiceLocator.GetService<LocalisationManager>().GetLocalisedString($"LevelUp_{num}");
            
            // Show the level up interface
            ToggleLevelUp(true);
            
            // Play the level up audio
            m_LevelUpAudioSource.Play();
        }

        public void OnStart()
        {
            
        }

        public void OnEnd()
        {
            
        }
    }
}
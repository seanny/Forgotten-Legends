//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using System.Collections;
using Core.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.MainMenu
{
    public class MenuLogo : Singleton<MenuLogo>
    {
        public bool isLogoFinished;
        public bool isLogoRunning;
        public Image splashImage;
        public GameObject mainMenu;
        public Image companyLogoNoFade;
        public TextMeshProUGUI versionText;
        public const string COMPANY_URL = "https://outlawgamesstudio.com";

        public Button continueButton;
        public Button newGameButton;
        public Button loadGameButton;
        public Button settingsButton;
        public Button creditsButton;
        public Button exitButton;

        private void Start()
        {
            ResetCompanyLogo();
            ResetMainMenu();
            StartCoroutine(ShowCompanyLogo());
        }

        private void ResetCompanyLogo()
        {;
            companyLogoNoFade.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(false);
            versionText.gameObject.SetActive(false);
            ImageUtils.SetAlpha(splashImage, 0.0f);
        }

        private void ResetMainMenu()
        {
            ImageUtils.SetAlpha(splashImage, 0.0f);
        }

        private IEnumerator ShowCompanyLogo()
        {
            isLogoRunning = true;
            yield return new WaitForSeconds(0.25f);
            ImageUtils.FadeAlpha(splashImage, 1.0f, 3f);
            yield return new WaitForSeconds(3f);

            ImageUtils.FadeAlpha(splashImage, 0.0f, 3f);
            yield return new WaitForSeconds(3f);

            isLogoFinished = true;
        }

        private void Update()
        {
            if(isLogoFinished)
            {
                FadeInMainMenu();
            }
        }

        private void FadeInMainMenu()
        {
            mainMenu.gameObject.SetActive(true);
            companyLogoNoFade.gameObject.SetActive(true);
            versionText.gameObject.SetActive(true);
            ImageUtils.FadeAlpha(continueButton, 1.0f, .5f);
            ImageUtils.FadeAlpha(newGameButton, 1.0f, .5f);
            ImageUtils.FadeAlpha(loadGameButton, 1.0f, .5f);
            ImageUtils.FadeAlpha(settingsButton, 1.0f, .5f);
            ImageUtils.FadeAlpha(creditsButton, 1.0f, .5f);
            ImageUtils.FadeAlpha(exitButton, 1.0f, .5f);
        }

        public void OpenWebsite()
        {
            Application.OpenURL(COMPANY_URL);
        }

        public void ContinueGame()
        {
            Logging.Log("Not implemented");
        }

        public void NewGame()
        {
            SceneManager.LoadScene("SampleScene");
        }

        public void LoadGame()
        {
            Logging.Log("Not implemented");
        }

        public void SettingsButton()
        {
            Logging.Log("Not implemented");
        }

        public void CreditsButton()
        {
            Logging.Log("Not implemented");
        }

        public void ExitButton()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();

#endif
        }
    }
}

//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MenuLogo : Singleton<MenuLogo>
{
    public bool isLogoFinished;
    public bool isLogoRunning;
    public Image splashImage;
    public GameObject mainMenu;
    public Image companyLogoNoFade;
    public TextMeshProUGUI versionText;
    public const string COMPANY_URL = "https://outlawgamesstudio.com";

    private void Start()
    {
        ResetCompanyLogo();
        StartCoroutine(ShowCompanyLogo());
    }

    private void ResetCompanyLogo()
    {
        companyLogoNoFade.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        versionText.gameObject.SetActive(false);
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
        mainMenu.SetActive(true);
        companyLogoNoFade.gameObject.SetActive(true);
        versionText.gameObject.SetActive(true);
    }
}

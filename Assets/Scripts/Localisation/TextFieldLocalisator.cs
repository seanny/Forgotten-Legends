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
using System.Collections;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Text))]
public class TextFieldLocalisator : MonoBehaviour
{
    [SerializeField] private string textKey;
    [SerializeField] private Font overrideFont;

    private Text textField;

    private void onLanguageChanged()
    {
        refresh();
    }

    void OnEnable () 
    {
        textField = GetComponent<Text>();
        LocalisationManager.Instance.OnLanguageChanged += onLanguageChanged;
        refresh();
    }

    void OnDisable()
    {
        LocalisationManager.Instance.OnLanguageChanged -= onLanguageChanged;
    }

    void OnDestroy()
    {
        LocalisationManager.Instance.OnLanguageChanged -= onLanguageChanged;
    }

    public void refresh()
    {
        refreshText();
        refreshFont();
    }

    public void refreshText()
    {
        if(!string.IsNullOrEmpty(textKey))
        {
            var s = LocalisationManager.Instance.getStringForKey(textKey);
            textField.text = s;
        }
    }

    public void refreshFont()
    {
        if(overrideFont != null)
        {
            textField.font = overrideFont;
        }
        else
        {
            var currentFont = LocalisationManager.Instance.getFontPerCurrentLanguage();
            if(currentFont != null)
                textField.font = currentFont;   
        }
    }
}

using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class LanguageEntry  
{
    [Tooltip("The related language name.")]
    [SerializeField] private SystemLanguage language;

    [Tooltip("The related translation CSV file.")]
    [SerializeField] private string translationCSVFile;

    [Tooltip("Optional custom Font to replace the default font in the manager.")]
    [SerializeField] private Font customFont;

    [Tooltip("Optional TextMeshPro custom Font to replace the default font in the manager.")]
    [SerializeField] private TMP_FontAsset customTextMeshFont;

    private Dictionary<string, string> translationDict;

    public void initialise()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Locale", translationCSVFile);
        if (!File.Exists(filePath))
        {
            Debug.LogError($"Missing translation file for {language}");
            return;
        }

        string data = File.ReadAllText(filePath);
        if(string.IsNullOrEmpty(data))
        {
            Debug.LogError($"{language}.csv appears to be an empty file.");
            return;
        }

        translationDict = CSVUtils.parseLocalisationFile(data);
        Logger.Log(Channel.Localisation, $"Loaded {language} with {translationDict.Count} entries");
    }

    public bool hasStringForKey(string key)
    {
        return translationDict.ContainsKey(key);
    }

    public string getStringForKey(string key)
    {
        string result = "";
        translationDict.TryGetValue(key, out result);
        return result;
    }
    
    public SystemLanguage getLanguage()
    {
        return language;
    }

    public Font getCustomFont()
    {
        return customFont;
    }

    public bool hasCustomFont()
    {
        return customFont != null;
    }

    public TMP_FontAsset getCustomTextMeshFont()
    {
        return customTextMeshFont;
    }

    public bool hasCustomTextMeshFont()
    {
        return customTextMeshFont != null;
    }
    
}
//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//
using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Settings Class, used to modify game settings.
/// </summary>
public class Settings : Singleton<Settings>
{
    /// <summary>
    /// Gets the config file full path.
    /// </summary>
    /// <value>Path to Settings.ini.</value>
    public string ConfigFile { get; private set; }

    /// <summary>
    /// Gets the path of the game data folder.
    /// </summary>
    /// <value>Game Data Directory.</value>
    public string FolderName { get; private set; }

    /// <summary>
    /// Game Folder Name
    /// </summary>
    /// <value>Forgotten Legends</value>
    public const string GAME_FOLDER_NAME = "Forgotten Legends";

    /// <summary>
    /// Config File Name
    /// </summary>
    /// <value>Settings.ini</value>
    public const string CONFIG_FILE_NAME = "Settings.ini";

    private void Start()
    {
        FolderName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", GAME_FOLDER_NAME);
        if (!Directory.Exists(FolderName))
        {
            Directory.CreateDirectory(FolderName);
        }

        ConfigFile = Path.Combine(FolderName, CONFIG_FILE_NAME);
        //if (!File.Exists(ConfigFile))
        //{

        //}
    }

    /// <summary>
    /// Set Integer Setting Value
    /// </summary>
    /// <param name="property">Property (iProperty).</param>
    /// <param name="value">Value.</param>
    public void SetProperty(string property, int value)
    {
        GamePrefs.ForInt().Set(property, value);
    }

    /// <summary>
    /// Gets the Setting property.
    /// </summary>
    /// <param name="property">Property.</param>
    public string GetProperty(string property)
    {
        return GamePrefs.ForString().Get(property);
    }

    /// <summary>
    /// Set boolean Setting Value
    /// </summary>
    /// <param name="property">Property (bProperty).</param>
    /// <param name="value"><see langword="true"/> or <see langword="false"/></param>
    public void SetProperty(string property, bool value)
    {
        GamePrefs.ForBool().Set(property, value);
    }

    /// <summary>
    /// Set Char Setting Value
    /// </summary>
    /// <param name="property">Property (cProperty).</param>
    /// <param name="value">Value.</param>
    public void SetProperty(string property, char value)
    {
        GamePrefs.ForChar().Set(property, value);
    }

    /// <summary>
    /// Set long Setting Value
    /// </summary>
    /// <param name="property">Property (lProperty).</param>
    /// <param name="value">Value.</param>
    public void SetProperty(string property, long value)
    {
        GamePrefs.ForLong().Set(property, value);
    }

    /// <summary>
    /// Set float Setting Value
    /// </summary>
    /// <param name="property">Property (fProperty).</param>
    /// <param name="value">Value.</param>
    public void SetProperty(string property, float value)
    {
        GamePrefs.ForFloat().Set(property, value);
    }

    /// <summary>
    /// Set double Setting Value
    /// </summary>
    /// <param name="property">Property (dProperty).</param>
    /// <param name="value">Value.</param>
    public void SetProperty(string property, double value)
    {
        GamePrefs.ForDouble().Set(property, value);
    }

    /// <summary>
    /// Set string Setting Value
    /// </summary>
    /// <param name="property">Property (sProperty).</param>
    /// <param name="value">Value.</param>
    public void SetProperty(string property, string value)
    {
        GamePrefs.ForString().Set(property, value);
    }

    /// <summary>
    /// Set DateTime Setting Value (sets as a long value)
    /// </summary>
    /// <param name="property">Property (lProperty).</param>
    /// <param name="value">Value.</param>
    public void SetProperty(string property, DateTime value)
    {
        GamePrefs.ForDateTime().Set(property, value);
    }
}

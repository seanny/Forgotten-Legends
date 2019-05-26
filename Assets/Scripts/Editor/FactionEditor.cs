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
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FactionEditor : EditorWindow
{
    string path = Path.Combine(Application.streamingAssetsPath, CoreFactions.FACTION_DIR);
    public string factionID;
    public string factionName;
    public string factionFileName;

    SerializedObject serializedObject;

    public List<string> factionEnemies;

    [MenuItem("Tools/Faction Editor")]
    static void Init()
    {
        FactionEditor window = (FactionEditor)EditorWindow.GetWindow(typeof(FactionEditor));
        window.Show();
    }

    void FactionEnemyList()
    {
        ScriptableObject target = this;
        SerializedObject reactionFac = new SerializedObject(target);
        SerializedProperty stringsProperty = reactionFac.FindProperty("factionEnemies");

        EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
        reactionFac.ApplyModifiedProperties(); // Remember to apply modified properties
    }

    private void SaveFactionButton()
    {
        if (GUILayout.Button("Save Faction"))
        {
            WriteFaction();
        }
    }

    private void LoadFactionButton()
    {
        if (GUILayout.Button("Load Faction"))
        {
            string file = EditorUtility.OpenFilePanel("Faction file.", path, "fctn");
            if (file.Length != 0)
            {
                var fileContent = File.ReadAllText(file);
                Faction faction = JsonUtility.FromJson<Faction>(fileContent);
                var fileName = Path.GetFileName(file);
                fileName = Path.ChangeExtension(fileName, null);

                factionID = faction.id;
                factionName = faction.name;
                factionFileName = fileName;
                factionEnemies = faction.enemyFactions;
            }
        }
    }

    private void ClearFactionButton()
    {
        if (GUILayout.Button("Clear Window"))
        {
            factionID = string.Empty;
            factionName = string.Empty;
            factionFileName = string.Empty;
            factionEnemies.Clear();
        }
    }

    private void FactionInfo()
    {
        GUILayout.Label("Faction Information", EditorStyles.boldLabel);
        factionID = EditorGUILayout.TextField("Faction ID", factionID);
        factionName = EditorGUILayout.TextField("Faction Name", factionName);
        factionFileName = EditorGUILayout.TextField("Faction File Name", factionFileName);
    }

    private void WriteFaction()
    {
        Faction faction = new Faction
        {
            id = factionID,
            name = factionName,
            enemyFactions = factionEnemies
        };
        string jsonData = JsonUtility.ToJson(faction);
        string finalPath = Path.Combine(path, $"{factionFileName}.fctn");
        File.WriteAllText(finalPath, jsonData);
        Debug.Log($"Faction written to \"{finalPath}\"");
    }

    private void OnGUI()
    {
        FactionInfo();
        FactionEnemyList();
        SaveFactionButton();
        LoadFactionButton();
        ClearFactionButton();
    }
}

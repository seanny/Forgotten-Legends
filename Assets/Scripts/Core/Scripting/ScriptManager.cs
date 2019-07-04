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
using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

public class ScriptManager : Singleton<ScriptManager>
{
    private LuaVM m_LuaVM;
    private string m_CoreScripts;
    private string m_ModScripts;
    private List<string> m_LoadOrder;

    public void CallFunction(string functionName)
    {
        for (int i = 0; i < m_LoadOrder.Count; i++)
        {
            m_LuaVM.ExecuteScript(m_LoadOrder[i]);
            DynValue function = m_LuaVM.GetGlobal(functionName);
            m_LuaVM.Call(function);
        }
    }

    public void CallFunction(string functionName, object[] paramaters)
    {
        for (int i = 0; i < m_LoadOrder.Count; i++)
        {
            m_LuaVM.ExecuteScript(m_LoadOrder[i]);
            DynValue function = m_LuaVM.GetGlobal(functionName);
            m_LuaVM.Call(function, paramaters);
        }
    }

    public void CallFunctionInScript(string scriptName, string functionName)
    {
        m_LuaVM.ExecuteScript(scriptName);
        DynValue function = m_LuaVM.GetGlobal(functionName);
        m_LuaVM.Call(function);
    }

    public void CallFunctionInScript(string scriptName, string functionName, object[] paramaters)
    {
        m_LuaVM.ExecuteScript(scriptName);
        DynValue function = m_LuaVM.GetGlobal(functionName);
        m_LuaVM.Call(function, paramaters);
    }

    private void InitCoreScripts()
    {
        AddScriptToLoadOrder(m_CoreScripts, "fruits.lua");
        AddScriptToLoadOrder(m_CoreScripts, "veg.lua");
    }

    private void AddScriptToLoadOrder(string folder, string scriptName)
    {
        m_LoadOrder.Add(Path.Combine(folder, scriptName));
        CallFunctionInScript(scriptName, "OnStart");
    }

    private void InitScriptManager()
    {
        m_LoadOrder = new List<string>();
        m_CoreScripts = Path.Combine(Application.streamingAssetsPath, "Scripts");
        if(!Directory.Exists(m_CoreScripts))
        {
            Debug.LogError($"Core Scripts directory does not exist: {m_CoreScripts}");
            return;
        }
    }

    private void InitLua()
    {
        m_LuaVM = new LuaVM();
    }

    private void InitRepeatingMethods()
    {
        StartCoroutine(OnUpdate());
    }

    IEnumerator OnUpdate()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            CallFunction("OnUpdate");
        }
    }

    private void Start()
    {
        InitScriptManager();
        InitLua();
        InitCoreScripts();
        InitRepeatingMethods();
    }
}

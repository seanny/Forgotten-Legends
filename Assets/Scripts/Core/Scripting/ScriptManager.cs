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

    /// <summary>
    /// Calls a function which is specified in functionName.
    /// This will search ALL scripts for said function.
    /// Function MUST exist otherwise an error will be thrown.
    /// </summary>
    /// <param name="functionName">Function name.</param>
    public void CallFunction(string functionName)
    {
        for (int i = 0; i < m_LoadOrder.Count; i++)
        {
            CallFunctionInScript(m_LoadOrder[i], functionName);
        }
    }

    /// <summary>
    /// Calls a function which is specified in functionName.
    /// This will search ALL scripts for said function.
    /// Function MUST exist otherwise an error will be thrown.
    /// </summary>
    /// <param name="functionName">Function name.</param>
    /// <param name="paramaters">Paramaters.</param>
    public void CallFunction(string functionName, object[] paramaters)
    {
        for (int i = 0; i < m_LoadOrder.Count; i++)
        {
            CallFunctionInScript(m_LoadOrder[i], functionName, paramaters);
        }
    }

    /// <summary>
    /// Calls a function which is specified in functionName inside of scriptName.
    /// This will search ALL scripts for said function.
    /// Function MUST exist otherwise an error will be thrown.
    /// </summary>
    /// <param name="scriptName">Script name.</param>
    /// <param name="functionName">Function name.</param>
    public void CallFunctionInScript(string scriptName, string functionName)
    {
        Logger.Log(Channel.Lua, $"Executing {scriptName}");
        m_LuaVM.ExecuteScript(Path.Combine(m_CoreScripts, scriptName));
        Logger.Log(Channel.Lua, $"Getting global {functionName} in {scriptName}");
        DynValue function = m_LuaVM.GetGlobal(functionName);
        if (function.IsNotNil())
        {
            Logger.Log(Channel.Lua, $"Executing global {functionName} in {scriptName}");
            m_LuaVM.Call(function);
        }
    }

    /// <summary>
    /// Calls a function which is specified in functionName inside of scriptName.
    /// This will search ALL scripts for said function.
    /// Function MUST exist otherwise an error will be thrown.
    /// </summary>
    /// <param name="scriptName">Script name.</param>
    /// <param name="functionName">Function name.</param>
    /// <param name="paramaters">Paramaters.</param>
    public void CallFunctionInScript(string scriptName, string functionName, object[] paramaters)
    {
        Logger.Log(Channel.Lua, $"Executing {scriptName} with paramaters");
        m_LuaVM.ExecuteScript(Path.Combine(m_CoreScripts, scriptName));
        Logger.Log(Channel.Lua, $"Getting global {functionName} in {scriptName} with paramaters");
        DynValue function = m_LuaVM.GetGlobal(functionName);
        if (function.IsNotNil())
        {
            Logger.Log(Channel.Lua, $"Executing global {functionName} in {scriptName} with paramaters");
            m_LuaVM.Call(function, paramaters);
        }
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
        Logger.Log(Channel.Loading, $"Added {scriptName} to load order");
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
        Debug.Log($"Core Scripts directory found: {m_CoreScripts}");
    }

    private void InitLua()
    {
        m_LuaVM = new LuaVM(LuaVM.VMSettings.AttachAPIs);
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

    IEnumerator OnStart()
    {
        yield return new WaitForSeconds(1f);
        InitScriptManager();
        InitLua();
        InitCoreScripts();
        InitRepeatingMethods();
    }

    private void Start()
    {
        StartCoroutine(OnStart());
    }
}

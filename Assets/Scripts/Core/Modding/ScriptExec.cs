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
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using UnityEngine;

public class ScriptExec : Singleton<ScriptExec>
{
    public string ModFolder { get; private set; }

    private const string CORE_DLL = "Assembly-CSharp.dll";
    private Assembly coreAssembly;
    private List<Assembly> assemblies;
    private string path;
    private BindingFlags bindingFlags =
                BindingFlags.Public |
                BindingFlags.DeclaredOnly |
                BindingFlags.Instance;

    private void Start()
    {
        InitModFolder();
        InitAssemblyList();
    }

    private void InitAssemblyList()
    {
        if (assemblies == null)
        {
            assemblies = new List<Assembly>();
        }
    }

    private void InitModFolder()
    {
        if(string.IsNullOrEmpty(ModFolder))
        {
            ModFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", "Forgotten Legends", "Mods");
            Debug.Log($"ModFolder: {ModFolder}");
        }
    }

    private void LoadCoreDLL(string methodName, object[] paramaters)
    {
        path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        Debug.Log($"Searching for assemblies in {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)} ...");
        foreach (string dll in Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories))
        {
            if (dll.Contains(CORE_DLL))
            {
                Debug.Log($"Found core dll: {dll}");
                coreAssembly = Assembly.LoadFile(dll);

                foreach (var type in coreAssembly.GetTypes())
                {
                    if (type.Name != "IDialogue")
                    {
                        InvokeMethod(type, methodName, paramaters);
                    }
                }
                break;
            }
        }
    }

    private void LoadModDLLs()
    {
        InitModFolder();
        InitAssemblyList();
        Debug.Log($"Searching mod folder {ModFolder}...");

        foreach (string dll in Directory.GetFiles(ModFolder, "*.dll", SearchOption.AllDirectories))
        {
            if (!dll.Contains(CORE_DLL))
            {
                Debug.Log($"Found mod dll: {dll}");
                assemblies.Add(Assembly.LoadFile(dll));
            }
        }
    }

    private void InvokeMethod(Type type, string methodName, object[] paramaters)
    {
        // Search for all methods of methodName.
        // There may be multiple assemblies with the same method name in one or more classes ...
        // ... because they implement interface IDialogue
        foreach (var method in type.GetMethods(bindingFlags))
        {
            // If the method matches the name, proceed otherwise keep looking.
            if (method.Name == methodName)
            {
                // Call the constructor
                ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
                object classObj = ctor.Invoke(new object[] { });

                // Call the specified method
                Debug.Log($"Calling {method.Name} from {type.Name}");
                method.Invoke(classObj, paramaters);
            }
        }
    }

    private void RunFunction(string methodName, object[] paramaters)
    {
        for (int i = 0; i < assemblies.Count; i++)
        {
            foreach (var type in assemblies[i].GetTypes())
            {
                foreach (var method in type.GetMethods(bindingFlags))
                {
                    InvokeMethod(type, methodName, paramaters);
                }
            }
        }
    }

    /// <summary>
    /// Run a method from inside one of the loaded game assemblies (mod or core).
    /// </summary>
    /// <param name="methodName">Method name.</param>
    /// <param name="paramaters">Paramaters.</param>
    public void RunMethod(string methodName, object[] paramaters)
    {
        LoadCoreDLL(methodName, paramaters);
        LoadModDLLs();
        RunFunction(methodName, paramaters);
    }
}

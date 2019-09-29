//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Core.Editor
{
    public static class PostProcessBuild
    {
        [PostProcessBuild(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            // We're only targeting x86_64 targets.
            if (target == BuildTarget.StandaloneWindows64 || target == BuildTarget.StandaloneLinux64 || target == BuildTarget.StandaloneOSX)
            {
                // Get build path
                string pureBuildPath = Path.GetDirectoryName(pathToBuiltProject);

                // Remove PDB files
                foreach (string file in Directory.GetFiles(pureBuildPath, "*.pdb"))
                {
                    Debug.Log(file + " deleted!");
                    File.Delete(file);
                }
            }
        }
    }
}

//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using Core.CommandConsole;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Debugging
{
    [RequireComponent(typeof(LineRenderer))]
    public class AIPath : MonoBehaviour
    {
        public LineRenderer line { private set; get; }
        Transform target;
        NavMeshAgent agent;
        static bool drawing;

        // Use this for initialization
        void Start()
        {
            line = GetComponent<LineRenderer>();
            target = GetComponent<Transform>();
            agent = GetComponent<NavMeshAgent>();
        }

        [RegisterCommand(Help = "Show AI Paths")]
        public static void CommandAIPath(CommandArg[] args)
        {
            AIPath[] aiPath = FindObjectsOfType<AIPath>();
            drawing = !drawing;
            if(drawing)
            {
                for (int i = 0; i < aiPath.Length; i++)
                {
                    aiPath[i].GetPath();
                }
            }
            else
            {
                for (int i = 0; i < aiPath.Length; i++)
                {
                    aiPath[i].line.positionCount = 0;
                }
            }
        }

        public void GetPath()
        {
            line.SetPosition(0, transform.position);
            DrawPath(agent.path);
        }

        void DrawPath(NavMeshPath path)
        {
            if(path.corners.Length < 2)
            {
                return;
            }

            line.positionCount = path.corners.Length;
            for (int i = 0; i < path.corners.Length; i++)
            {
                line.SetPosition(i, path.corners[i]);
            }
        }
    }
}

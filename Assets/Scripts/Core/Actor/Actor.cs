//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using Core.Inventory;
using UnityEngine;

namespace Core.Actor
{
    public abstract class Actor : MonoBehaviour
    {
        public string actorID;
        public ActorStats m_ActorStats;
        public ActorClass m_ActorClass;
        public ActorHealth m_HealthScript;
        public EntityInventory actorInventory;
        public string actorWorldspace;

        public static Actor GetPlayer()
        {
            return FindActor("Player");
        }

        public void SetPos(Vector3 position)
        {
            gameObject.transform.position = position;
        }

        public void SetPos(float x, float y, float z)
        {
            gameObject.transform.position = new Vector3(x, y, z);
        }

        public static Actor FindActor(string actorID)
        {
            Actor[] actors = GameObject.FindObjectsOfType<Actor>();
            foreach (Actor actor in actors)
            {
                if (actor.actorID == actorID)
                {
                    return actor;
                }  
            }
            return null;
        }

        protected virtual void Start()
        {
            if(actorID == string.Empty)
            {
                actorID = gameObject.name;
            }

            actorInventory = gameObject.AddComponent<EntityInventory>();
        }
    }
}

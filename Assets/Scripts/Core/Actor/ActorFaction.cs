using System;
using System.Collections.Generic;
using Core.Factions;
using Core.Services;
using UnityEngine;

namespace Core.Actor
{
    public class ActorFaction
    {
        public List<Faction> actorFactions { get; private set; }

        public ActorFaction()
        {
            // Make sure that CoreFactions service is running.
            
        }

        public void AssignFaction(Faction faction)
        {
            actorFactions.Add(faction);
        }

        public void RemoveFaction(Faction faction)
        {
            actorFactions.Remove(faction);
        }
    }
}
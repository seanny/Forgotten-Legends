using System.Diagnostics;
using Core.Services;

namespace Core.Stats
{
    public class StatControlService : IService
    {
        public void OnStart()
        {
            
        }

        public void OnEnd()
        {
            
        }
        
        public bool SetActorStat(Actor.Actor actor, string statID, int value)
        {
            BaseStat baseStat = actor.actorStatController.GetStat(statID);
            if (baseStat != null)
            {
                baseStat.statValue = value;
                return true;
            }
            return false;
        }
    }
}
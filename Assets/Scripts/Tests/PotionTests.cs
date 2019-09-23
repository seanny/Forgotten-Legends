using Core.Actor;
using Core.NonPlayerChar;
using NUnit.Framework;
using Core.Potion;
using Core.Services;
using UnityEngine;

namespace Tests
{
    public class PotionTests
    {
        [Test]
        public void HealthEffectTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<NPC>();
            ActorHealth actorHealth = gameObject.AddComponent<ActorHealth>();
            
            NPC actor = gameObject.GetComponent<NPC>();
            Debug.Log($"Current Health: {actorHealth.currentHealth}");
            actorHealth.AddHealth(100);
//            ServiceLocator.GetService<PotionEffects>().AddHealthEffect(50, actor);
            Debug.Log($"Current Health: {actor.m_HealthScript.currentHealth}");
            if (actor.m_HealthScript.currentHealth >= 100)
            {
                Assert.Pass();
            }
            else Assert.Fail();
        }
    }
}
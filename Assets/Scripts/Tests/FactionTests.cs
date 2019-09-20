using System.Collections.Generic;
using NUnit.Framework;
using Core.Factions;
using Core.Services;
using UnityEngine;

namespace Tests
{
    public class FactionTests
    {
        [Test]
        public void LoadFactionTest()
        {
            bool found = false;
            ServiceLocator.GetService<CoreFactions>();
            List<Faction> factions = ServiceLocator.GetService<CoreFactions>().factions;
            foreach (var faction in factions)
            {
                if (faction.name == "TestFaction")
                {
                    Debug.Log($"Found Faction: {faction.name}");
                    found = true;
                }
            }
            if (found)
            {
                Assert.Pass();
            }
            else Assert.Fail();
        }
    }
}